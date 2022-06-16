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
    public class Main : IExceptionHandler, IExceptionLogger, IFillContainer, INotifyPropertyChanged
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
        public Container<Product> ProductContainer { get; private set; } = new Container<Product>();

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
            StockEntryNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance -= x.Entry));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance -= x.Entry));
            });
            StockEntryNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += x.Entry));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += x.Entry));
            });
            StockEntryNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += x.Entry - y.Entry));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += x.Entry - y.Entry));
            });
            StockEntryManager = new StockEntryManager(StockEntryNonRetentiveRepo);
            #endregion
            #region SiloManager
            var SiloRepo = new EntityRepository<SiloDTO, ExpertContext>();
            var SiloRetentiveRepo = new RetentiveSiloRepository(SiloRepo, Mapper);
            SiloRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                y.Do(z => StockManager.Get(stock => stock.StockId == z.StockId).Do(stock => SiloManager.Get(silo => silo.SiloId == z.SiloId).Do(silo => stock.RemoveSilo(silo))));
                x.Do(z => StockManager.Get(stock => stock.StockId == z.StockId).Do(stock => SiloManager.Get(silo => silo.SiloId == z.SiloId).Do(silo => stock.AddSilo(silo))));
                //x.Do(z => _stockManager.Get(stock => stock.StockId == z.StockId).Do(stock => stock.AddSilo(x)));
                //y.Stock.Do(t => t.RemoveSilo(y));
                //x.Stock = _stockManager.Get(stock => stock.StockId == x.StockId);
                CheckSiloInStock(x);
            });
            SiloRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, y) =>
            {
                x.DoIf(z => z.Enabled.GetBool() && y.Stock != null, t =>
                {
                    t.IsStock = "true";
                    bool activeFound = false;
                    foreach (var item in t.Stock.Silos)
                    {
                        if (item != null && !item.Equals(t))
                        {
                            if (item.IsActive.GetBool())
                            {
                                activeFound = true;
                                break;
                            }
                        }
                    }
                    if (!activeFound)
                        t.IsActive = "true";
                }
                );
            });
            SiloManager = new SiloManager(SiloRetentiveRepo);
            #endregion
            #region BatchedStockManager
            var BatchedStockRepo = new EntityRepository<BatchedStockDTO, ExpertContext>();
            var BatchedStockNonRetentiveRepo = new NonRetentiveBatchedStockRepository(BatchedStockRepo, Mapper);
            BatchedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += x.Batched));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += x.Batched));
            });
            BatchedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance -= x.Batched));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance -= x.Batched));
            });
            BatchedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += y.Batched - x.Batched));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += y.Batched - x.Batched));
            });
            BatchedStockManager = new BatchedStockManager(BatchedStockNonRetentiveRepo);
            #endregion
            #region ConsumedStockManager
            var ConsumedStockRepo = new EntityRepository<ConsumedStockDTO, ExpertContext>();
            var ConsumedStockNonRetentiveRepo = new NonRetentiveConsumedStockRepository(ConsumedStockRepo, Mapper);
            ConsumedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += x.Consumed));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += x.Consumed));
            });
            ConsumedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance -= x.Consumed));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance -= x.Consumed));
            });
            ConsumedStockNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                StockManager.GetList(z => z.StockId == x.StockId)?.ForEach(z => z.Do(t => t.Balance += y.Consumed - x.Consumed));
                SiloManager.GetList(z => z.SiloId == x.SiloId)?.ForEach(z => z.Do(t => t.Balance += y.Consumed - x.Consumed));
            });
            ConsumedStockManager = new ConsumedStockManager(ConsumedStockNonRetentiveRepo);
            #endregion
            #region DriverManager
            var DriverRepo = new EntityRepository<DriverDTO, ExpertContext>();
            var DriverRetentiveRepo = new RetentiveDriverRepository(DriverRepo, Mapper);
            DriverRetentiveRepo.TriggerPeformer.CreateTrigger().After().Delete().Action((x, y) =>
            {
                ServiceManager.GetList(z => z.DriverId == x.DriverId)?.ForEach(z => z.Do(t => t.Driver = null));
            });
            DriverRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                x.DoIf(q => q.IsHidden.GetBool(), w => ServiceManager.GetList(z => z.DriverId == x.DriverId)?.ForEach(z => z.Do(t => t.Driver = null)));
            });
            DriverManager = new DriverManager(DriverRetentiveRepo);
            #endregion
            #region ServiceManager
            var ServiceRepo = new EntityRepository<ServiceDTO, ExpertContext>();
            var ServiceRetentiveRepo = new RetentiveServiceRepository(ServiceRepo, Mapper);
            ServiceRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, y) =>
            {
                x.ServiceCode.DoIf(z => !string.IsNullOrEmpty(z), z => x.ServiceName = z);
                x.LicencePlate.DoIf(z => !string.IsNullOrEmpty(z), z => x.ServiceName += " " + z);
            });
            ServiceManager = new ServiceManager(ServiceRetentiveRepo);
            #endregion
            #region ProductManager
            var ProductRepo = new EntityRepository<ProductDTO, ExpertContext>();
            var ProductNonRetentiveRepo = new NonRetentiveProductRepository(ProductRepo, Mapper);
            ProductNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Action((x, y) =>
            {
                OrderManager.Do(orderManager => orderManager.Get(order => order.OrderId == x.OrderId).Do(order => order.Produced += x.Shipped));
            });
            ProductNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Update().Action((x, y) =>
            {
                OrderManager.Do(orderManager => orderManager.Get(order => order.OrderId == x.OrderId).Do(order =>
                {
                    order.AddWater = x.AddWater;
                    order.PumpServiceId = x.PumpServiceId;
                    order.ServiceCatNum = x.ServiceCatNum;
                }));
                x.DoIf(z => z.Pos >= 3, z => ProductContainer.Unregister(z));
                FillProduct(x);
            });
            ProductNonRetentiveRepo.TriggerPeformer.CreateTrigger().After().Update().Action((x, y) =>
            {
                x.DoIfElse(z =>
                {
                    int id = -1;
                    y.Do(yy => id = yy.OrderId);
                    return !z.OrderId.IsEqual(id);
                }, z =>
                OrderManager.Do(orderManager => orderManager.Get(order => order.OrderId == x.OrderId).Do(order => order.Produced += x.Shipped - y.Shipped)), z =>
                {
                    OrderManager.Do(orderManager => orderManager.Get(order => order.OrderId == x.OrderId).Do(order => order.Produced += x.Shipped));
                    OrderManager.Do(orderManager => orderManager.Get(order => order.OrderId == y.OrderId).Do(order => order.Produced -= y.Shipped));
                });
            });
            ProductNonRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, y) =>
            {
                x.RealTarget = x.Target - x.Addon;
                x.Shipped = x.Produced + x.Addon;
                x.Recycled = x.Returned + x.Transfer;
                x.Delivered = x.Shipped - x.Returned;

                x.DoIfElse(z => z.Capacity > 0, z => z.AimBatch = (int)Math.Ceiling(z.RealTarget / z.Capacity), z => z.AimBatch = 0);
                x.DoIfElse(z => z.AimBatch > 0, z => z.Ubm = z.RealTarget / ((double)z.AimBatch), z => z.Ubm = 0);

            });
            ProductManager = new ProductManager(ProductNonRetentiveRepo);
            #endregion
            #region OrderManager
            var OrderRepo = new EntityRepository<OrderDTO, ExpertContext>();
            var OrderRetentiveRepo = new RetentiveOrderRepository(OrderRepo, Mapper);
            OrderRetentiveRepo.TriggerPeformer.CreateTrigger().Before().Insert().Update().Action((x, y) =>
            {
                x.Remaining = x.Quantity - x.Produced;
            });
            OrderRetentiveRepo.TriggerPeformer.CreateTrigger().After().Insert().Update().Action((x, y) =>
            {
                x.OrderDate.DoIf(date => date.IsEarlierThan(date.MakeFirstDate().AddDays(-7)), date => x.OrderStatusEnum = OrderStatus.ZamanAşımı);
                x.OrderDate.DoIf(date => date.IsEarlierThan(date.MakeFirstDate().AddDays(-14)), date => OrderManager.Delete(x));
                FillOrderProperties(x);
            });
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

            InitEntities();

        }
        public void FillContainers(object item)
        {
            item.DoIf(x => x is ICommander, x => CommanderContainer.Register(x as ICommander));
            item.DoIf(x => x is IVariableOwner, x => VariableOwnerContainer.Register(x as IVariableOwner));
            item.DoIf(x => x is IParameterOwner, x => ParameterOwnerContainer.Register(x as IParameterOwner));
            item.DoIf(x => x is IAnimationObject, x => AnimationObjectContainer.Register(x as IAnimationObject));
        }
        private void CheckSiloInStock(Silo x)
        {
            x.DoIf(z => !z.Enabled.GetBool() || !z.IsStock.GetBool(), t => StockManager.Get(z => z.StockId == t.StockId).Do(z => z.RemoveSilo(t)));
            x.DoIf(z => z.Enabled.GetBool() || z.IsStock.GetBool(), t => StockManager.Get(z => z.StockId == t.StockId).Do(z => z.AddSilo(t)));
        }
        private void FillProduct(Product x)
        {
            x.Do(y => ClientManager.Get(client => client.ClientId == y.ClientId && !client.IsHidden.GetBool()).Do(z => y.Client = z));
            x.Do(y => SiteManager.Get(site => site.SiteId == y.SiteId && !site.IsHidden.GetBool()).Do(z => y.Site = z));
            x.Do(y => OrderManager.Get(order => order.OrderId == y.OrderId).Do(z => y.Order = z));
            x.Do(y => RecipeManager.Get(recipe => recipe.RecipeId == y.RecipeId && !recipe.IsHidden.GetBool()).Do(z => y.Recipe = z));
            x.Do(y => ServiceCategoryManager.Get(serviceCategory => serviceCategory.CatNum == y.ServiceCatNum).Do(z => y.ServiceCategory = z));
            x.Do(y => ServiceManager.Get(service => service.ServiceId == y.ServiceId && !service.IsHidden.GetBool()).Do(z => y.Service = z));
            x.Do(y => DriverManager.Get(driver => driver.DriverId == y.DriverId && !driver.IsHidden.GetBool()).Do(z => y.Driver = z));
        }
        private void FillOrderProperties(Order order)
        {
            order.Do(x => x.Client = ClientManager.Get(y => y.ClientId == x.ClientId));
            order.Do(x => x.Site = SiteManager.Get(y => y.SiteId == x.SiteId));
            order.Do(x => x.Recipe = RecipeManager.Get(y => y.RecipeId == x.RecipeId));
            order.Do(x => x.ServiceCategory = ServiceCategoryManager.Get(y => y.CatNum == x.ServiceCatNum));
        }
        void InitEntities()
        {
            ServiceCategoryManager.GetList();
            ClientManager.GetList()?.ForEach(x =>
            {
                SiteManager.GetList(y => x.ClientId == y.ClientId).ForEach(t => t.Do(z => x.AddSite(z)));
                SentViewReader.GetList(" where CLIENT_ID='" + x.ClientId.ToString() + "' and PRODUCT_DATE between '" + DateTime.Now.MakeFirstDate().StartOfYear().ToString("yyyy-MM-dd") + "' and '" + DateTime.Now.MakeLastDate().ToString("yyyy-MM-dd") + "'")?.ForEach(y => x.Do(z => z.Delivered += y.Delivered));
            });
            StockManager.GetList()?.ForEach(x =>
            {
                SiloManager.GetList(y => x.StockId == y.StockId && y.Enabled.GetBool() && y.IsStock.GetBool()).ForEach(t => t.Do(z => x.AddSilo(z)));
                SiloManager.GetList(y => y.StockCatNum == x.StockCatNum).ForEach(y => y.Do(silo => silo.AvailableStocks.Add(x)));
            });
            RecipeManager.GetList()?.ForEach(x =>
            {
                RecipeContentManager.GetList(y => x.RecipeId == y.RecipeId).ForEach(t => t.Do(z => x.AddRecipeContent(z)));
            });
            RecipeContentManager.GetList()?.ForEach(x =>
            {
                StockManager.Get(y => y.StockId == x.StockId).Do(y => x.Stock = y);
            });
            ServiceManager.GetList()?.ForEach(x =>
            {
                DriverManager.Get(y => x.DriverId == y.DriverId && !y.IsHidden.GetBool()).Do(t => x.Driver = t);
            });
            UserManager.GetList();
            OrderManager.GetList().ForEach(x =>
            {
                x.Do(y => ClientManager.Get(client => client.ClientId == y.ClientId && !client.IsHidden.GetBool()).Do(z => y.Client = z));
                x.Do(y => SiteManager.Get(site => site.SiteId == y.SiteId && !site.IsHidden.GetBool()).Do(z => y.Site = z));
                x.Do(y => RecipeManager.Get(recipe => recipe.RecipeId == y.RecipeId && !recipe.IsHidden.GetBool()).Do(z => y.Recipe = z));
                x.Do(y => ServiceCategoryManager.Get(serviceCategory => serviceCategory.CatNum == y.ServiceCatNum).Do(z => y.ServiceCategory = z));
            });
            ProductManager.GetList(x=>x.Pos < 3).ForEach(x =>
            {
                ProductContainer.Register(x);
                FillProduct(x);
            });
        }
    }
}
