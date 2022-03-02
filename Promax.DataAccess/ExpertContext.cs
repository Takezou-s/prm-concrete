using Promax.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.DataAccess
{
    public class ExpertContext : DbContext
    {
        public DbSet<ProductWayBillViewDTO> WayBills { get; set; }
        public DbSet<NormViewDTO> NormViews { get; set; }
        public DbSet<ClientDTO> Clients { get; set; }
        public DbSet<DriverDTO> Drivers { get; set; }
        public DbQuery<BatchedStockDTO> InvBatcheds { get; set; }
        public DbSet<ConsumedStockDTO> InvConsumeds { get; set; }
        public DbSet<StockEntryDTO> InvEntries { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<SiloDTO> SiloParameters { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<RecipeDTO> Recipes { get; set; }
        public DbSet<ServiceDTO> Services { get; set; }
        public DbSet<SiteDTO> Sites { get; set; }
        public DbSet<StockDTO> Stocks { get; set; }
        public DbSet<ServiceCategoryDTO> SysCatsSet { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RecipeContentDTO> RecipeContents { get; set; }
        public ExpertContext() : base(Infrastructure.Firebird)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

        }
        public ExpertContext(string name) : base(name)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            var wayBill = modelBuilder.Entity<ProductWayBillViewDTO>();
            wayBill.ToTableExt<ProductWayBillViewDTO>();
            wayBill.HasKey(x => x.ProductId);
            wayBill.Property(x => x.ProductId).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ProductId));
            wayBill.Property(x => x.ProductDate).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ProductDate));
            wayBill.Property(x => x.ProductTime).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ProductTime));
            wayBill.Property(x => x.DepTime).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.DepTime));
            wayBill.Property(x => x.RtmNumber).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.RtmNumber));
            wayBill.Property(x => x.BillNumber).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.BillNumber));
            wayBill.Property(x => x.Shipped).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Shipped));
            wayBill.Property(x => x.Delivered).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Delivered));
            wayBill.Property(x => x.TotalMaterial).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.TotalMaterial));
            wayBill.Property(x => x.ClientName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ClientName));
            wayBill.Property(x => x.ClientAddress).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ClientAddress));
            wayBill.Property(x => x.ClientPhone).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ClientPhone));
            wayBill.Property(x => x.ClientTaxLocation).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ClientTaxLocation));
            wayBill.Property(x => x.ClientTaxNumber).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ClientTaxNumber));
            wayBill.Property(x => x.SiteName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.SiteName));
            wayBill.Property(x => x.SiteAddress).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.SiteAddress));
            wayBill.Property(x => x.SitePhone).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.SitePhone));
            wayBill.Property(x => x.SiteContact).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.SiteContact));
            wayBill.Property(x => x.RecipeName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.RecipeName));
            wayBill.Property(x => x.Consistency).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Consistency));
            wayBill.Property(x => x.Endurance).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Endurance));
            wayBill.Property(x => x.Dmax).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Dmax));
            wayBill.Property(x => x.CementType).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.CementType));
            wayBill.Property(x => x.MineralType).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.MineralType));
            wayBill.Property(x => x.AdditiveType).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.AdditiveType));
            wayBill.Property(x => x.Slump).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Slump));
            wayBill.Property(x => x.UnitVolume).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.UnitVolume));
            wayBill.Property(x => x.Environmental).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.Environmental));
            wayBill.Property(x => x.ChlorideContent).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ChlorideContent));
            wayBill.Property(x => x.CemRate).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.CemRate));
            wayBill.Property(x => x.MixerServiceId).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.MixerServiceId));
            wayBill.Property(x => x.MixerLicencePlate).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.MixerLicencePlate));
            wayBill.Property(x => x.MixerDriver).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.MixerDriver));
            wayBill.Property(x => x.PumpServiceId).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.PumpServiceId));
            wayBill.Property(x => x.PumpLicencePlate).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.PumpLicencePlate));
            wayBill.Property(x => x.PumpDriver).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.PumpDriver));
            wayBill.Property(x => x.ServiceCategoryName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.ServiceCategoryName));
            wayBill.Property(x => x.UserName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.UserName));
            wayBill.Property(x => x.AdditiveName).HasColumnNameExt<ProductWayBillViewDTO>(nameof(ProductWayBillViewDTO.AdditiveName));
            #region NormView
            var normView = modelBuilder.Entity<NormViewDTO>();
            normView.ToTableExt<NormViewDTO>();
            normView.HasKey(x => x.RecipeId);
            normView.Property(x => x.RecipeId).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.RecipeId));
            normView.Property(x => x.TotalAggregate).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.TotalAggregate));
            normView.Property(x => x.TotalCement).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.TotalCement));
            normView.Property(x => x.TotalWater).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.TotalWater));
            normView.Property(x => x.TotalAdditive).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.TotalAdditive));
            normView.Property(x => x.TotalMaterial).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.TotalMaterial));
            normView.Property(x => x.CementRate).HasColumnNameExt<NormViewDTO>(nameof(NormViewDTO.CementRate));
            #endregion
            #region Client
            var client = modelBuilder.Entity<ClientDTO>();
            client.ToTableExt<ClientDTO>();
            client.HasKey(x => x.ClientId);
            client.Property(x => x.ClientId).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.ClientId));
            client.Property(x => x.ClientCode).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.ClientCode));
            client.Property(x => x.ClientName).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.ClientName));
            client.Property(x => x.Address).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.Address));
            client.Property(x => x.State).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.State));
            client.Property(x => x.City).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.City));
            client.Property(x => x.AddressLine).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.AddressLine));
            client.Property(x => x.Contact).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.Contact));
            client.Property(x => x.Phone).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.Phone));
            client.Property(x => x.TaxLocation).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.TaxLocation));
            client.Property(x => x.TaxNumber).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.TaxNumber));
            client.Property(x => x.Gravity).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.Gravity));
            client.Property(x => x.IsHidden).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.IsHidden));
            client.Property(x => x.ClientType).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.ClientType));
            client.Property(x => x.FirstName).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.FirstName));
            client.Property(x => x.LastName).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.LastName));
            client.Property(x => x.ClientTitle).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.ClientTitle));
            client.Property(x => x.Email).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.Email));
            client.Property(x => x.EnableNotification).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.EnableNotification));
            client.Property(x => x.MailAttachInfo).HasColumnNameExt<ClientDTO>(nameof(ClientDTO.MailAttachInfo));
            #endregion
            #region Driver
            var driverEntity = modelBuilder.Entity<DriverDTO>();
            driverEntity.ToTableExt<DriverDTO>();
            driverEntity.HasKey(x => x.DriverId);
            driverEntity.Property(x => x.DriverId).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.DriverId));
            driverEntity.Property(x => x.DriverCode).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.DriverCode));
            driverEntity.Property(x => x.DriverName).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.DriverName));
            driverEntity.Property(x => x.Identity).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.Identity));
            driverEntity.Property(x => x.BloodGroup).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.BloodGroup));
            driverEntity.Property(x => x.Address).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.Address));
            driverEntity.Property(x => x.State).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.State));
            driverEntity.Property(x => x.City).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.City));
            driverEntity.Property(x => x.Phone).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.Phone));
            driverEntity.Property(x => x.Gravity).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.Gravity));
            driverEntity.Property(x => x.IsHidden).HasColumnNameExt<DriverDTO>(nameof(DriverDTO.IsHidden));
            #endregion
            #region InvBatched
            var invBatchedEntity = modelBuilder.Entity<BatchedStockDTO>();
            invBatchedEntity.ToTableExt<BatchedStockDTO>();
            invBatchedEntity.HasKey(x => x.BatchedId);
            invBatchedEntity.Property(x => x.BatchedId).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.BatchedId));
            invBatchedEntity.Property(x => x.ProductId).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.ProductId));
            invBatchedEntity.Property(x => x.BatchNo).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.BatchNo));
            invBatchedEntity.Property(x => x.StockId).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.StockId));
            invBatchedEntity.Property(x => x.SiloId).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.SiloId));
            invBatchedEntity.Property(x => x.UserId).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.UserId));
            invBatchedEntity.Property(x => x.BatchedDate).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.BatchedDate));
            invBatchedEntity.Property(x => x.BatchedTime).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.BatchedTime));
            invBatchedEntity.Property(x => x.AddVal).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.AddVal));
            invBatchedEntity.Property(x => x.Design).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.Design));
            invBatchedEntity.Property(x => x.Batched).HasColumnNameExt<BatchedStockDTO>(nameof(BatchedStockDTO.Batched));
            #endregion
            #region InvConsumed
            var invConsumedEntity = modelBuilder.Entity<ConsumedStockDTO>();
            invConsumedEntity.ToTableExt<ConsumedStockDTO>();
            invConsumedEntity.HasKey(x => x.ConsumedId);
            invConsumedEntity.Property(x => x.ConsumedId).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.ConsumedId));
            invConsumedEntity.Property(x => x.StockId).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.StockId));
            invConsumedEntity.Property(x => x.SiloId).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.SiloId));
            invConsumedEntity.Property(x => x.UserId).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.UserId));
            invConsumedEntity.Property(x => x.ConsumedDate).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.ConsumedDate));
            invConsumedEntity.Property(x => x.ConsumedTime).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.ConsumedTime));
            invConsumedEntity.Property(x => x.Consumed).HasColumnNameExt<ConsumedStockDTO>(nameof(ConsumedStockDTO.Consumed));
            #endregion
            #region InvEntry
            var invEntryEntity = modelBuilder.Entity<StockEntryDTO>();
            invEntryEntity.ToTableExt<StockEntryDTO>();
            invEntryEntity.HasKey(x => x.EntryId);
            invEntryEntity.Property(x => x.EntryId).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.EntryId));
            invEntryEntity.Property(x => x.StockId).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.StockId));
            invEntryEntity.Property(x => x.SiloId).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.SiloId));
            invEntryEntity.Property(x => x.UserId).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.UserId));
            invEntryEntity.Property(x => x.EntryDate).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.EntryDate));
            invEntryEntity.Property(x => x.EntryTime).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.EntryTime));
            invEntryEntity.Property(x => x.DocumentNo).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.DocumentNo));
            invEntryEntity.Property(x => x.Description).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.Description));
            invEntryEntity.Property(x => x.Entry).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.Entry));
            invEntryEntity.Property(x => x.Minus).HasColumnNameExt<StockEntryDTO>(nameof(StockEntryDTO.Minus));
            #endregion
            #region Order
            var orderEntity = modelBuilder.Entity<OrderDTO>();
            orderEntity.ToTableExt<OrderDTO>();
            orderEntity.HasKey(x => x.OrderId);
            orderEntity.Property(x => x.OrderId).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.OrderId));
            orderEntity.Property(x => x.OrderDate).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.OrderDate));
            orderEntity.Property(x => x.OrderTime).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.OrderTime));
            orderEntity.Property(x => x.OrderStatus).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.OrderStatus));
            orderEntity.Property(x => x.ServiceCatNum).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.ServiceCatNum));
            orderEntity.Property(x => x.ClientId).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.ClientId));
            orderEntity.Property(x => x.SiteId).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.SiteId));
            orderEntity.Property(x => x.RecipeId).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.RecipeId));
            orderEntity.Property(x => x.AddWater).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.AddWater));
            orderEntity.Property(x => x.PumpServiceId).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.PumpServiceId));
            orderEntity.Property(x => x.Quantity).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.Quantity));
            orderEntity.Property(x => x.Produced).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.Produced));
            orderEntity.Property(x => x.Remaining).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.Remaining));
            orderEntity.Property(x => x.OrderNumber).HasColumnNameExt<OrderDTO>(nameof(OrderDTO.OrderNumber));
            #endregion
            #region PrmSilos
            var prmSilosEntity = modelBuilder.Entity<SiloDTO>();
            prmSilosEntity.ToTableExt<SiloDTO>();
            prmSilosEntity.HasKey(x => x.SiloId);
            prmSilosEntity.Property(x => x.SiloId).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SiloId));
            prmSilosEntity.Property(x => x.WegId).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.WegId));
            prmSilosEntity.Property(x => x.SiloName).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SiloName));
            prmSilosEntity.Property(x => x.SiloNo).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SiloNo));
            prmSilosEntity.Property(x => x.IsStock).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.IsStock));
            prmSilosEntity.Property(x => x.IsActive).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.IsActive));
            prmSilosEntity.Property(x => x.StockId).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.StockId));
            prmSilosEntity.Property(x => x.Capacity).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.Capacity));
            prmSilosEntity.Property(x => x.Scale).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.Scale));
            prmSilosEntity.Property(x => x.FastVal).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.FastVal));
            prmSilosEntity.Property(x => x.VibOn).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.VibOn));
            prmSilosEntity.Property(x => x.VibOff).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.VibOff));
            prmSilosEntity.Property(x => x.VibFl).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.VibFl));
            prmSilosEntity.Property(x => x.SwingOn).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SwingOn));
            prmSilosEntity.Property(x => x.SwingOff).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SwingOff));
            prmSilosEntity.Property(x => x.SwingVal).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.SwingVal));
            prmSilosEntity.Property(x => x.TolVal).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.TolVal));
            prmSilosEntity.Property(x => x.ShotVal).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.ShotVal));
            prmSilosEntity.Property(x => x.ManNem).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.ManNem));
            prmSilosEntity.Property(x => x.NemId).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.NemId));
            prmSilosEntity.Property(x => x.MinDebi).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.MinDebi));
            prmSilosEntity.Property(x => x.Enabled).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.Enabled));
            prmSilosEntity.Property(x => x.Temp).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.Temp));
            prmSilosEntity.Property(x => x.Balance).HasColumnNameExt<SiloDTO>(nameof(Entities.SiloDTO.Balance));
            #endregion
            #region Product
            var productEntity = modelBuilder.Entity<ProductDTO>();
            productEntity.ToTableExt<ProductDTO>();
            productEntity.HasKey(x => x.ProductId);
            productEntity.Property(x => x.ProductId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.ProductId));
            productEntity.Property(x => x.ProductDate).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.ProductDate));
            productEntity.Property(x => x.ProductTime).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.ProductTime));
            productEntity.Property(x => x.RtmNumber).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.RtmNumber));
            productEntity.Property(x => x.BillNumber).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.BillNumber));
            productEntity.Property(x => x.OrderId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.OrderId));
            productEntity.Property(x => x.ClientId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.ClientId));
            productEntity.Property(x => x.SiteId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.SiteId));
            productEntity.Property(x => x.RecipeId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.RecipeId));
            productEntity.Property(x => x.ServiceCatNum).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.ServiceCatNum));
            productEntity.Property(x => x.MixerServiceId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.MixerServiceId));
            productEntity.Property(x => x.PumpServiceId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.PumpServiceId));
            productEntity.Property(x => x.SelfServiceId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.SelfServiceId));
            productEntity.Property(x => x.MixerDriverId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.MixerDriverId));
            productEntity.Property(x => x.PumpDriverId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.PumpDriverId));
            productEntity.Property(x => x.SelfDriverId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.SelfDriverId));
            productEntity.Property(x => x.UserId).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.UserId));
            productEntity.Property(x => x.Target).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Target));
            productEntity.Property(x => x.Addon).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Addon));
            productEntity.Property(x => x.RealTarget).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.RealTarget));
            productEntity.Property(x => x.Produced).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Produced));
            productEntity.Property(x => x.Shipped).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Shipped));
            productEntity.Property(x => x.Returned).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Returned));
            productEntity.Property(x => x.Transfer).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Transfer));
            productEntity.Property(x => x.Recycled).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Recycled));
            productEntity.Property(x => x.Delivered).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Delivered));
            productEntity.Property(x => x.Capacity).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Capacity));
            productEntity.Property(x => x.Ubm).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Ubm));
            productEntity.Property(x => x.AimBatch).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.AimBatch));
            productEntity.Property(x => x.AimFactor).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.AimFactor));
            productEntity.Property(x => x.RtmBatch).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.RtmBatch));
            productEntity.Property(x => x.RtmFactor).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.RtmFactor));
            productEntity.Property(x => x.GateNum).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.GateNum));
            productEntity.Property(x => x.AddWater).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.AddWater));
            productEntity.Property(x => x.IsBill).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.IsBill));
            productEntity.Property(x => x.Desk).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Desk));
            productEntity.Property(x => x.Dep).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Dep));
            productEntity.Property(x => x.DepTime).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.DepTime));
            productEntity.Property(x => x.Pos).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.Pos));
            productEntity.Property(x => x.DespatchNumber).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.DespatchNumber));
            productEntity.Property(x => x.DespatchGuid).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.DespatchGuid));
            productEntity.Property(x => x.EbisNumber).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.EbisNumber));
            productEntity.Property(x => x.DespatchTag).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.DespatchTag));
            productEntity.Property(x => x.DespatchStatus).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.DespatchStatus));
            productEntity.Property(x => x.OrderQuantity).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.OrderQuantity));
            productEntity.Property(x => x.OrderNumber).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.OrderNumber));
            productEntity.Property(x => x.OrderDate).HasColumnNameExt<ProductDTO>(nameof(ProductDTO.OrderDate));
            #endregion
            #region Recipe
            var recipeEntity = modelBuilder.Entity<RecipeDTO>();
            recipeEntity.ToTableExt<RecipeDTO>();
            recipeEntity.HasKey(x => x.RecipeId);
            recipeEntity.Property(x => x.RecipeId).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.RecipeId));
            recipeEntity.Property(x => x.RecipeCode).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.RecipeCode));
            recipeEntity.Property(x => x.RecipeCatNum).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.RecipeCatNum));
            recipeEntity.Property(x => x.RecipeName).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.RecipeName));
            recipeEntity.Property(x => x.Cweg1OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Cweg1OrderTime));
            recipeEntity.Property(x => x.Cweg2OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Cweg2OrderTime));
            recipeEntity.Property(x => x.Sweg1OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Sweg1OrderTime));
            recipeEntity.Property(x => x.Sweg2OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Sweg2OrderTime));
            recipeEntity.Property(x => x.Kweg1OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Kweg1OrderTime));
            recipeEntity.Property(x => x.Kweg2OrderTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Kweg2OrderTime));
            recipeEntity.Property(x => x.MixingTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.MixingTime));
            recipeEntity.Property(x => x.FullopenTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.FullopenTime));
            recipeEntity.Property(x => x.LastopenTime).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.LastopenTime));
            recipeEntity.Property(x => x.UnitPrice).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.UnitPrice));
            recipeEntity.Property(x => x.CementType).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.CementType));
            recipeEntity.Property(x => x.MineralType).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.MineralType));
            recipeEntity.Property(x => x.AdditiveType).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.AdditiveType));
            recipeEntity.Property(x => x.Consistency).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Consistency));
            recipeEntity.Property(x => x.Endurance).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Endurance));
            recipeEntity.Property(x => x.Dmax).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Dmax));
            recipeEntity.Property(x => x.Slump).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Slump));
            recipeEntity.Property(x => x.UnitVolume).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.UnitVolume));
            recipeEntity.Property(x => x.Environmental).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Environmental));
            recipeEntity.Property(x => x.ChlorideContent).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.ChlorideContent));
            recipeEntity.Property(x => x.Gravity).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Gravity));
            recipeEntity.Property(x => x.IsHidden).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.IsHidden));
            recipeEntity.Property(x => x.EnduranceRate).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.EnduranceRate));
            recipeEntity.Property(x => x.Fibers).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.Fibers));
            recipeEntity.Property(x => x.EnduranceDay).HasColumnNameExt<RecipeDTO>(nameof(RecipeDTO.EnduranceDay));
            #endregion
            #region Service
            var serviceEntity = modelBuilder.Entity<ServiceDTO>();
            serviceEntity.ToTableExt<ServiceDTO>();
            serviceEntity.HasKey(x => x.ServiceId);
            serviceEntity.Property(x => x.ServiceId).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.ServiceId));
            serviceEntity.Property(x => x.ServiceCode).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.ServiceCode));
            serviceEntity.Property(x => x.ServiceName).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.ServiceName));
            serviceEntity.Property(x => x.ServiceCatNum).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.ServiceCatNum));
            serviceEntity.Property(x => x.LicencePlate).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.LicencePlate));
            serviceEntity.Property(x => x.DriverId).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.DriverId));
            serviceEntity.Property(x => x.Capacity).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.Capacity));
            serviceEntity.Property(x => x.Gravity).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.Gravity));
            serviceEntity.Property(x => x.IsHidden).HasColumnNameExt<ServiceDTO>(nameof(ServiceDTO.IsHidden));
            #endregion
            #region Site
            var siteEntity = modelBuilder.Entity<SiteDTO>();
            siteEntity.ToTableExt<SiteDTO>();
            siteEntity.HasKey(x => x.SiteId);
            siteEntity.Property(x => x.SiteId).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.SiteId));
            siteEntity.Property(x => x.ClientId).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.ClientId));
            siteEntity.Property(x => x.SiteCode).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.SiteCode));
            siteEntity.Property(x => x.SiteName).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.SiteName));
            siteEntity.Property(x => x.Address).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.Address));
            siteEntity.Property(x => x.State).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.State));
            siteEntity.Property(x => x.City).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.City));
            siteEntity.Property(x => x.AddressLine).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.AddressLine));
            siteEntity.Property(x => x.Contact).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.Contact));
            siteEntity.Property(x => x.Phone).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.Phone));
            siteEntity.Property(x => x.Distance).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.Distance));
            siteEntity.Property(x => x.IsHidden).HasColumnNameExt<SiteDTO>(nameof(SiteDTO.IsHidden));
            #endregion
            #region Stock
            var stockEntity = modelBuilder.Entity<StockDTO>();
            stockEntity.ToTableExt<StockDTO>();
            stockEntity.HasKey(x => x.StockId);
            stockEntity.Property(x => x.StockId).HasColumnNameExt<StockDTO>(nameof(StockDTO.StockId));
            stockEntity.Property(x => x.StockCode).HasColumnNameExt<StockDTO>(nameof(StockDTO.StockCode));
            stockEntity.Property(x => x.StockName).HasColumnNameExt<StockDTO>(nameof(StockDTO.StockName));
            stockEntity.Property(x => x.StockCatNum).HasColumnNameExt<StockDTO>(nameof(StockDTO.StockCatNum));
            stockEntity.Property(x => x.Temp).HasColumnNameExt<StockDTO>(nameof(StockDTO.Temp));
            stockEntity.Property(x => x.Balance).HasColumnNameExt<StockDTO>(nameof(StockDTO.Balance));
            #endregion
            #region SysCats
            var sysCatsEntity = modelBuilder.Entity<ServiceCategoryDTO>();
            sysCatsEntity.ToTableExt<ServiceCategoryDTO>();
            sysCatsEntity.HasKey(x => x.CatId);
            sysCatsEntity.Property(x => x.CatId).HasColumnNameExt<ServiceCategoryDTO>(nameof(ServiceCategoryDTO.CatId));
            sysCatsEntity.Property(x => x.CatClass).HasColumnNameExt<ServiceCategoryDTO>(nameof(ServiceCategoryDTO.CatClass));
            sysCatsEntity.Property(x => x.CatNum).HasColumnNameExt<ServiceCategoryDTO>(nameof(ServiceCategoryDTO.CatNum));
            sysCatsEntity.Property(x => x.CatName).HasColumnNameExt<ServiceCategoryDTO>(nameof(ServiceCategoryDTO.CatName));
            #endregion
            #region User
            var userEntity = modelBuilder.Entity<UserDTO>();
            userEntity.ToTableExt<UserDTO>();
            userEntity.HasKey(x => x.UserId);
            userEntity.Property(x => x.UserId).HasColumnNameExt<UserDTO>(nameof(UserDTO.UserId));
            userEntity.Property(x => x.UserCatNum).HasColumnNameExt<UserDTO>(nameof(UserDTO.UserCatNum));
            userEntity.Property(x => x.UserName).HasColumnNameExt<UserDTO>(nameof(UserDTO.UserName));
            userEntity.Property(x => x.UserPassword).HasColumnNameExt<UserDTO>(nameof(UserDTO.UserPassword));
            userEntity.Property(x => x.FullName).HasColumnNameExt<UserDTO>(nameof(UserDTO.FullName));
            userEntity.Property(x => x.LastLogin).HasColumnNameExt<UserDTO>(nameof(UserDTO.LastLogin));
            userEntity.Property(x => x.AuOrders).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuOrders));
            userEntity.Property(x => x.AuProducts).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuProducts));
            userEntity.Property(x => x.AuClients).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuClients));
            userEntity.Property(x => x.AuRecipes).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuRecipes));
            userEntity.Property(x => x.AuStocks).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuStocks));
            userEntity.Property(x => x.AuServices).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuServices));
            userEntity.Property(x => x.AuManAgg).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuManAgg));
            userEntity.Property(x => x.AuManCem).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuManCem));
            userEntity.Property(x => x.AuManAdv).HasColumnNameExt<UserDTO>(nameof(UserDTO.AuManAdv));
            userEntity.Property(x => x.IsHidden).HasColumnNameExt<UserDTO>(nameof(UserDTO.IsHidden));
            #endregion
            #region RecipeContent
            var recipeContentEntity = modelBuilder.Entity<RecipeContentDTO>();
            recipeContentEntity.ToTableExt<RecipeContentDTO>();
            recipeContentEntity.HasKey(x => x.ContentId);
            recipeContentEntity.Property(x => x.ContentId).HasColumnNameExt<RecipeContentDTO>(nameof(RecipeContentDTO.ContentId));
            recipeContentEntity.Property(x => x.RecipeId).HasColumnNameExt<RecipeContentDTO>(nameof(RecipeContentDTO.RecipeId));
            recipeContentEntity.Property(x => x.StockId).HasColumnNameExt<RecipeContentDTO>(nameof(RecipeContentDTO.StockId));
            recipeContentEntity.Property(x => x.Quantity).HasColumnNameExt<RecipeContentDTO>(nameof(RecipeContentDTO.Quantity));
            #endregion
        }
    }
}