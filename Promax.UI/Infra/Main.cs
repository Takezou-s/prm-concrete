using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Promax.Business;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Extensions;
using System.Windows;
using Utility.Binding;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.UI
{
    public class Main : IExceptionHandler, IExceptionLogger, INotifyPropertyChanged
    {
        private List<string> _loggedStackTraces = new List<string>();
        private MyBinding _binding;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Log, Handle
        public void HandleExceptions(Action action)
        {
            try
            {
                action();
            }
            catch (RemoteVariableException exception)
            {
                VariableException(exception);
                LogException(exception);
            }
            catch (Exception exception)
            {
                MessageBox.Show(GetExceptionMessage(exception), "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                LogException(exception);
            }
        }
        public void HandleExceptionsNotNotify(Action action)
        {
            try
            {
                action();
            }
            catch (RemoteVariableException exception)
            {
                VariableException(exception);
                LogException(exception);
            }
            catch (Exception exception)
            {
                LogException(exception);
            }
        }

        private void VariableException(RemoteVariableException exception)
        {
            exception.Variable.DoIf(x =>
            {
                bool result = false;
                RemoteVariableExceptionHandler.DoIf(y => y.PendingVariables.Contains(x), y => result = true);
                //RetentiveParameters.DoIf(y => y.IsObjectIncluded(x), y => result = true);
                //RecipeScope.DoIf(y => y.IsObjectIncluded(x), y => result = true);
                //CommandScope.DoIf(y => y.IsObjectIncluded(x) && y.IsCommandRetentive(x), y => result = true);
                return result;
            }, x =>
            {
                RemoteVariableExceptionHandler.DoIfElse(handler => handler.Contains(x),
                    handler => handler.Set(x, exception.Action, exception.Operation),
                    handler => handler.Add(x, exception.Action, exception.Operation));
            });
        }

        public void LogException(Exception exception)
        {
            Logger.DoIf(x =>
            {
                string stackTrace = exception.StackTrace;
                var objInList = _loggedStackTraces.FirstOrDefault(y => y.IsEqual(stackTrace));
                bool result = objInList == null;
                return result;
            }, x =>
            {
                x.DoReturn(y =>
                {
                    Exception Ex = new Exception(Environment.NewLine + exception.Message, exception);
                    y.Error(Ex, Ex.Message);
                }).Do(y =>
                {
                    _loggedStackTraces.Add(exception.StackTrace);
                });
            });
        }
        public string GetExceptionMessage(Exception exception)
        {
            string result = null;
            //if (exception is FluentValidation.ValidationException)
            //{
            //    result = exception.Message;
            //    return result;
            //}
            if (exception.Message.Contains("ERR_REFERENTIAL_INTEGRITY"))
            {
                result = "İşlem Gören Kayıtlar Silinemez";
                return result;
            }
            if (exception.InnerException != null)
            {
                result = GetExceptionMessage(exception.InnerException);
                return result;
            }
            result = exception.Message;
            return result;
        }
        public void Handle(Action action)
        {
            HandleExceptions(action);
        }
        void IExceptionLogger.Log(Action action)
        {
            HandleExceptionsNotNotify(action);
        }
        #endregion
        public IBeeMapper Mapper { get; private set; }
        public IBeeLogger Logger { get; private set; }
        public RemoteVariableExceptionHandler RemoteVariableExceptionHandler { get; private set; }

        #region Managers
        public IClientManager ClientManager { get; private set; }
        public ISiteManager SiteManager { get; private set; }
        public IRecipeManager RecipeManager { get; private set; }
        public IRecipeContentManager RecipeContentManager { get; private set; }
        public IStockManager StockManager { get; private set; }
        public IStockEntryManager StockEntryManager { get; private set; }
        public ISiloManager SiloManager { get; private set; }
        public IBatchedStockManager BatchedStockManager { get; private set; }
        public IConsumedStockManager ConsumedStockManager { get; private set; }
        public IDriverManager DriverManager { get; private set; }
        public IServiceManager ServiceManager { get; private set; }
        public IProductManager ProductManager { get; private set; }
        public IOrderManager OrderManager { get; private set; }
        public IUserManager UserManager { get; private set; }
        public IServiceCategoryManager ServiceCategoryManager { get; private set; }
        public INormViewManager NormViewManager { get; private set; }
        public ISentViewReader SentViewReader { get; private set; }
        public IStockMovementViewReader StockEntryViewReader { get; private set; }
        public IStockMovementViewReader BatchedStockViewReader { get; private set; }
        public IStockMovementViewReader ConsumedStockViewReader { get; private set; }
        #endregion
        public IBeeValidator<Product> ProductValidator { get; private set; }


        public Settings Settings { get; private set; }
        public RetainVariableController RetainVariableController { get; private set; }
        public Serializer Serializer { get; private set; }

        public EasyModbusCommunicator VariableCommunicator { get; private set; }
        public VariableScope VariableScope { get; private set; }
        public RetentiveParameterScope RetentiveParameters { get; private set; }
        public RecipeScope RecipeScope { get; private set; }
        public CommandScope CommandScope { get; private set; }

        public ObjectContainer ObjectContainer { get { return (ObjectContainer)App.Current.Resources["ObjectContainer1"]; } }
        public AnimationObjectContainer AnimationObjectContainer { get; private set; } = new AnimationObjectContainer();
        public VariableOwnerContainer VariableOwnerContainer { get; private set; } = new VariableOwnerContainer();
        public ParameterOwnerContainer ParameterOwnerContainer { get; private set; } = new ParameterOwnerContainer();
        public CommanderContainer CommanderContainer { get; private set; } = new CommanderContainer();
        public EntitiesWithStringKeyContainer EntityContainer { get; private set; } = new EntitiesWithStringKeyContainer();

        public MyParameterDomain ParameterDomain { get; private set; }
        public VariableController VariableController { get; private set; }
        public void Init()
        {
            _binding = new MyBinding();
            Mapper = new MyMapper();
            Logger = new NLogger();
            
            #region ClientManager
            var clientRepo = new EntityRepository<ClientDTO, ExpertContext>();
            var clientRetentiveRepo = new RetentiveClientRepository(clientRepo, Mapper);
            clientRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, x1) =>
            {
                x.Address.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine = y.Trim());
                x.State.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine += " " + y.Trim());
                x.City.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine += " " + y.Trim());

                x.ClientType.DoIf(y => y == 0, y => x.FirstName = string.Empty);
                x.ClientType.DoIf(y => y == 0, y => x.LastName = string.Empty);
                x.ClientType.DoIf(y => y == 1, y => x.ClientTitle = string.Empty);

                x.FirstName.DoIf(y => x.ClientType == 1 && !string.IsNullOrEmpty(y.Trim()), y => x.ClientName = y.Trim());
                x.LastName.DoIf(y => x.ClientType == 1 && !string.IsNullOrEmpty(y.Trim()), y => x.ClientName += " " + y.Trim());

                x.ClientTitle.DoIf(y => x.ClientType == 0 && !string.IsNullOrEmpty(y.Trim()), y => x.ClientName = y.Trim());
            });
            ClientManager = new ClientManager(clientRetentiveRepo);
            #endregion
            #region SiteManager
            var siteRepo = new EntityRepository<SiteDTO, ExpertContext>();
            var siteRetentiveRepo = new RetentiveSiteRepository(siteRepo, Mapper);
            siteRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, x1) =>
            {
                x.Address.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine = y.Trim());
                x.State.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine += " " + y.Trim());
                x.City.DoIf(y => string.IsNullOrEmpty(y.Trim()), y => x.AddressLine += " " + y.Trim());
            });
            siteRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                x.Client.Do(z => z.RemoveSite(x));
            });
            siteRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, x1) =>
            {
                x.IsHidden.DoIf(y => y.GetBool(), y => x1.Client.Do(z => z.UpdateAtList(x1)));
            });
            siteRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Action((x, x1) =>
            {
                x.IsHidden.DoIf(y => !y.GetBool(), y => ClientManager.Get(z => z.ClientId == x.ClientId)?.AddSite(x));
            });
            SiteManager = new SiteManager(siteRetentiveRepo);
            #endregion
            #region RecipeManager
            var RecipeRepo = new EntityRepository<RecipeDTO, ExpertContext>();
            var RecipeRetentiveRepo = new RetentiveRecipeRepository(RecipeRepo, Mapper);
            RecipeManager = new RecipeManager(RecipeRetentiveRepo);
            #endregion
            #region RecipeContentManager
            var RecipeContentRepo = new EntityRepository<RecipeContentDTO, ExpertContext>();
            var RecipeContentRetentiveRepo = new RetentiveRecipeContentRepository(RecipeContentRepo, Mapper);
            RecipeContentManager = new RecipeContentManager(RecipeContentRetentiveRepo);
            #endregion
            #region StockManager
            var StockRepo = new EntityRepository<StockDTO, ExpertContext>();
            var StockRetentiveRepo = new RetentiveStockRepository(StockRepo, Mapper);
            StockRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                x.ClearSilos();
                SiloManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Stock = null));
                RecipeContentManager.GetList(z => z.StockId == x.StockId)?.ForEach(z =>
                    z.Do(t =>
                    {
                        t.Recipe?.RemoveRecipeContent(t);
                        RecipeContentManager.Delete(t);
                    }));
            });
            StockManager = new StockManager(StockRetentiveRepo);
            #endregion
            #region StockEntryManager
            var StockEntryRepo = new EntityRepository<StockEntryDTO, ExpertContext>();
            var StockEntryNonRetentiveRepo = new NonRetentiveStockEntryRepository(StockEntryRepo, Mapper);
            StockEntryManager = new StockEntryManager(StockEntryNonRetentiveRepo);
            #endregion
            #region SiloManager
            var SiloRepo = new EntityRepository<SiloDTO, ExpertContext>();
            var SiloRetentiveRepo = new RetentiveSiloRepository(SiloRepo, Mapper);
            SiloManager = new SiloManager(SiloRetentiveRepo);
            #endregion
            #region BatchedStockManager
            var BatchedStockRepo = new EntityRepository<BatchedStockDTO, ExpertContext>();
            var BatchedStockNonRetentiveRepo = new NonRetentiveBatchedStockRepository(BatchedStockRepo, Mapper);
            BatchedStockManager = new BatchedStockManager(BatchedStockNonRetentiveRepo);
            #endregion
            #region ConsumedStockManager
            var ConsumedStockRepo = new EntityRepository<ConsumedStockDTO, ExpertContext>();
            var ConsumedStockNonRetentiveRepo = new NonRetentiveConsumedStockRepository(ConsumedStockRepo, Mapper);
            ConsumedStockManager = new ConsumedStockManager(ConsumedStockNonRetentiveRepo);
            #endregion
            #region DriverManager
            var DriverRepo = new EntityRepository<DriverDTO, ExpertContext>();
            var DriverRetentiveRepo = new RetentiveDriverRepository(DriverRepo, Mapper);
            DriverManager = new DriverManager(DriverRetentiveRepo);
            #endregion
            #region ServiceManager
            var ServiceRepo = new EntityRepository<ServiceDTO, ExpertContext>();
            var ServiceRetentiveRepo = new RetentiveServiceRepository(ServiceRepo, Mapper);
            ServiceManager = new ServiceManager(ServiceRetentiveRepo);
            #endregion
            #region ProductManager
            var ProductRepo = new EntityRepository<ProductDTO, ExpertContext>();
            var ProductNonRetentiveRepo = new NonRetentiveProductRepository(ProductRepo, Mapper);
            ProductManager = new ProductManager(ProductNonRetentiveRepo);
            #endregion
            #region OrderManager
            var OrderRepo = new EntityRepository<OrderDTO, ExpertContext>();
            var OrderRetentiveRepo = new RetentiveOrderRepository(OrderRepo, Mapper);
            OrderManager = new OrderManager(OrderRetentiveRepo);
            #endregion
            #region UserManager
            var UserRepo = new EntityRepository<UserDTO, ExpertContext>();
            var UserRetentiveRepo = new RetentiveUserRepository(UserRepo, Mapper);
            UserManager = new UserManager(UserRetentiveRepo);
            #endregion
            #region ServiceCategoryManager
            var ServiceCategoryRepo = new EntityRepository<ServiceCategoryDTO, ExpertContext>();
            var ServiceCategoryRetentiveRepo = new RetentiveServiceCategoryRepository(ServiceCategoryRepo, Mapper);
            ServiceCategoryManager = new ServiceCategoryManager(ServiceCategoryRetentiveRepo);
            #endregion
            #region NormViewManager
            var NormViewRepo = new EntityRepository<NormViewDTO, ExpertContext>();
            var NormViewNonRetentiveRepo = new NonRetentiveNormViewRepository(NormViewRepo, Mapper);
            NormViewManager = new NormViewManager(NormViewNonRetentiveRepo);
            #endregion

            SentViewReader = new SentViewReader("VIEW_RTM_SEND");
            StockEntryViewReader = new StockMovementViewReader("VIEW_INV_ENTRY");
            BatchedStockViewReader = new StockMovementViewReader("VIEW_INV_ENTRY");
            ConsumedStockViewReader = new StockMovementViewReader("VIEW_INV_ENTRY");

            ProductValidator = new FluentProductValidator();

            Settings = new Settings();
            Serializer = new Serializer("saves\\Settings.json");
            RetainVariableController = new RetainVariableController(Serializer, Settings);

            VariableCommunicator = new EasyModbusCommunicator(this);
            VariableScope = new VariableScope(VariableCommunicator);
            VariableCommunicator.SetVariables(VariableScope);
            RemoteVariableExceptionHandler = new RemoteVariableExceptionHandler(VariableCommunicator);

            RecipeScope = new RecipeScope(VariableCommunicator);

            RetentiveParameters = new RetentiveParameterScope(VariableCommunicator);

            CommandScope = new CommandScope(VariableCommunicator, CommanderContainer);


            ParameterDomain = new MyParameterDomain(RetentiveParameters, ParameterOwnerContainer);
            ParameterDomain.ParameterOwnerContext.ParameterOwners.ForEach(x => FillContainers(x));

            VariableController = new VariableController(VariableCommunicator, VariableScope, VariableOwnerContainer, RemoteVariableExceptionHandler, BackgroundProcessor.GetProcessor());
            VariableController.Ip = Settings.Ip;
            VariableController.Timeout = Settings.Timeout;
            _binding.CreateBinding().Source(Settings).SourceProperty(nameof(Settings.Ip)).Target(VariableController).TargetProperty(nameof(VariableController.Ip)).Mode(MyBindingMode.OneWay);
            _binding.CreateBinding().Source(Settings).SourceProperty(nameof(Settings.Timeout)).Target(VariableController).TargetProperty(nameof(VariableController.Timeout)).Mode(MyBindingMode.OneWay);

        }
        public void FillContainers(object item)
        {
            item.DoIf(x => x is ICommander, x => CommanderContainer.Register(x as ICommander));
            item.DoIf(x => x is IVariableOwner, x => VariableOwnerContainer.Register(x as IVariableOwner));
            item.DoIf(x => x is IParameterOwner, x => ParameterOwnerContainer.Register(x as IParameterOwner));
            item.DoIf(x => x is IAnimationObject, x => AnimationObjectContainer.Register(x as IAnimationObject));
        }
    }
}
