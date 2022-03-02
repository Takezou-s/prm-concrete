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
        public ISentViewReader SentViewReader { get; private set; }
        public INormViewManager NormViewManager { get; private set; }
        #endregion
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
            SentViewReader = new SentViewReader("VIEW_RTM_SEND");
            #region NormViewManager
            var NormViewRepo = new EntityRepository<NormViewDTO, ExpertContext>();
            var NormViewNonRetentiveRepo = new NonRetentiveNormViewRepository(NormViewRepo, Mapper);
            NormViewManager = new NormViewManager(NormViewNonRetentiveRepo);
            #endregion
        }
    }
}
