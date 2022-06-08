using Extensions;
using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    /// <summary>
    /// Siloların çalışmasını kontrol eder.
    /// </summary>
    public class SiloController : VirtualPLCObject, IMalzemeBoşalt, IVariableOwner, ICommander
    {
        #region VirutalPLCProperty'ler
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }
        public VirtualPLCProperty SaveBatchedProperty { get; private set; }
        public VirtualPLCProperty İstenenProperty { get; private set; }
        #endregion
        #region Senaryo adımlarını belirten değişkenler.
        private readonly int _boşaltKomutuSenaryo = 0;
        private readonly int _boşaltıldıİzleSenaryo = 1;
        private readonly int _boşaltıldıResetleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3;
        private readonly int _batchKaydedildiİzleSenaryo = 4;
        #endregion
        /// <summary>
        /// Malzeme boşaltım senaryosunun adımını tutan değişken.
        /// </summary>
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }
        /// <summary>
        /// Gönderilecek parametrelerin bulunduğu ParameterOwner.
        /// </summary>
        public ParameterOwnerBase ParameterOwner { get; private set; }
        /// <summary>
        /// Parametre değişkenlerinin bulunduğu IVariables objesi.
        /// </summary>
        public IVariables ParameterScope { get; private set; }
        /// <summary>
        /// Reçete değişkenlerinin bulunduğu IVariables objesi.
        /// </summary>
        public IVariables RecipeScope { get; private set; }
        /// <summary>
        /// Reçete değişkenlerine erişirken kullanılacak isim.
        /// </summary>
        public string NameInRecipeScope { get; set; }
        /// <summary>
        /// Parametre değişkenlerine erişirken kullanılacak isim.
        /// </summary>
        public string NameInParameterScope { get; set; }
        /// <summary>
        /// Malzeme boşaltımının tamamlandığını belirtir.
        /// </summary>
        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        /// <summary>
        /// Malzeme boşaltımının yapılıyor olduğunu belirtir.
        /// </summary>
        public bool MalzemeBoşaltılıyor { get => MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo; }
        /// <summary>
        /// Dışarıdan gelen "Boşaltıldı" bilgisi. Boşalt komutu verildikten sonra bu bilgi takip edilir.
        /// </summary>
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        /// <summary>
        /// Boşaltım işlemi bittikten sonra batch'in kaydedilmesi gerektiğini belirten değişken.
        /// </summary>
        public bool SaveBatched { get => (bool)GetValue(SaveBatchedProperty); set => SetValue(SaveBatchedProperty, value); }
        /// <summary>
        /// Boşaltılması gereken miktar.
        /// </summary>
        public double İstenen { get => (double)GetValue(İstenenProperty); set => SetValue(İstenenProperty, value); }

        public SiloController(VirtualController controller, ParameterOwnerBase parameterOwner, string nameInRecipeScope, string nameInParameterScope, string variableOwnerName, string commanderName, IVariables parameterScope, IVariables recipeScope) : base(controller)
        {
            ParameterOwner = parameterOwner;
            ParameterScope = parameterScope;
            RecipeScope = recipeScope;
            NameInRecipeScope = nameInRecipeScope;
            NameInParameterScope = nameInParameterScope;
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            var builder = new VirtualPLCPropertyBuilder(this);
            MalzemeBoşaltıldıProperty = builder.Reset().Name(nameof(MalzemeBoşaltıldı)).Type(typeof(bool)).Retain(true).Get();
            MalzemeBoşaltSenaryoProperty = builder.Reset().Name(nameof(MalzemeBoşaltSenaryo)).Type(typeof(int)).Retain(true).Get();
            EjectedInfoProperty = builder.Reset().Name(nameof(EjectedInfo)).Type(typeof(bool)).Input(true).Retain(true).Get();
            SaveBatchedProperty = builder.Reset().Name(nameof(SaveBatched)).Type(typeof(bool)).Input(true).Retain(true).Get();
            İstenenProperty = builder.Reset().Name(nameof(İstenen)).Type(typeof(double)).Retain(true).Output(true).Get();
            ParameterOwner.Parameters.ForEach(x => x.PropertyChanged += ParameterPropertyChanged);
        }
        /// <summary>
        /// Parametre değeri değişmesi durumunda, malzeme boşaltımı yapılıyorsa değer PLC'ye yazılır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParameterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.IsEqual(nameof(IParameter.Value)) || !MalzemeBoşaltılıyor)
                return;
            WriteParameter(sender as IParameter);
        }
        /// <summary>
        /// Malzeme boşaltım işlemiyle ilgilenen method. Malzeme boşaltımı tamamlanmışsa işlem yapmaz.
        /// </summary>
        public void MalzemeBoşalt()
        {
            //Boşaltıldı ise geri dön.
            if (MalzemeBoşaltıldı)
                return;
            if (MalzemeBoşaltSenaryo == _boşaltKomutuSenaryo)
            {
                //İstenen miktar <= 0 ise boşaltıldı setlenir ve geri döner.
                if (İstenen <= 0)
                {
                    MalzemeBoşaltıldı = true;
                    return;
                }
                //İstenen değer yazılır.
                RecipeScope.Write(NameInRecipeScope, "İstenen", İstenen);
                //Parametreler yazılır.
                WriteParameters();
                //Boşalt komutu verilir.
                InvokeCommand(CommandNames.EjectCommand);
                //Bir sonraki senaryo adımına geçilir.
                MalzemeBoşaltSenaryo = _boşaltıldıİzleSenaryo;
            }
            //Boşaltıldı bilgisi geliyor ise bir sonraki adıma geçilir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                    MalzemeBoşaltSenaryo = _boşaltıldıResetleSenaryo;
            }
            //Boşaltıldı bilgisi resetlenir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetleSenaryo)
            {
                InvokeCommand(CommandNames.EjectedInfoResponse);
                MalzemeBoşaltSenaryo = _boşaltıldıResetKontrolSenaryo;
            }
            //Boşaltıldı bilgisi resetlendiyse batch kaydet setlenir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetKontrolSenaryo)
            {
                if (!EjectedInfo)
                {
                    SaveBatched = true;
                    MalzemeBoşaltSenaryo = _batchKaydedildiİzleSenaryo;
                }
            }
            //Save batched dışarıda işlenip, resetlendiyse malzeme boşaltıldı setlenir.
            else if (MalzemeBoşaltSenaryo == _batchKaydedildiİzleSenaryo)
            {
                if (!SaveBatched)
                    MalzemeBoşaltıldı = true;
            }
        }
        /// <summary>
        /// Tekrar malzeme boşaltmaya hazır hale gelir. Periyot resetlenmez!
        /// </summary>
        public void ResetMalzemeBoşalt()
        {
            MalzemeBoşaltSenaryo = 0;
            MalzemeBoşaltıldı = false;
        }
        /// <summary>
        /// Parametreleri yazar.
        /// </summary>
        private void WriteParameters()
        {
            ParameterOwner.Parameters.ForEach(x => WriteParameter(x));
        }
        /// <summary>
        /// Parametreyi yazar.
        /// </summary>
        /// <param name="parameter"></param>
        private void WriteParameter(IParameter parameter)
        {
            ParameterScope.Write(NameInParameterScope, parameter.Code, parameter.Value);
        }
        #region IVariableOwner
        private ConverterContainer _variableConverters = new ConverterContainer();
        private List<string> _variables = new List<string>();

        public string VariableOwnerName { get; set; }
        IEnumerable<string> IVariableOwner.Variables => _variables;

        IVariableOwner IVariableOwner.SetConverter(string propertyName, IBeeValueConverter converter)
        {
            _variableConverters.SetConverter(propertyName, converter);
            return this;
        }

        IBeeValueConverter IVariableOwner.GetConverterForVariable(string propertyName)
        {
            return _variableConverters.GetConverter(propertyName);
        }
        private void InitVariables()
        {
            _variables.Add(nameof(EjectedInfo));
        }
        #endregion
        #region ICommander
        private Commander _commander = new Commander();

        public string CommanderName { get => ((ICommander)_commander).CommanderName; set => ((ICommander)_commander).CommanderName = value; }
        public IReadOnlyDictionary<string, MyCommand> Commands => ((ICommander)_commander).Commands;

        public void InvokeCommand(string command, object[] parameters = null)
        {
            ((ICommander)_commander).InvokeCommand(command, parameters);
        }
        private void InitCommands()
        {
            _commander.RegisterCommand(CommandNames.EjectCommand);
            _commander.RegisterCommand(CommandNames.EjectedInfoResponse);
        }
        #endregion
    }
}
