using AutoMapper;
using Promax.Entities;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, User>();

            CreateMap<SiloDTO, Silo>();
            CreateMap<Silo, SiloDTO>();
            CreateMap<SiloDTO, SiloDTO>();
            CreateMap<Silo, Silo>().IgnoreMembers(x=>x.Stock, x=>x.AvailableStocks);

            CreateMap<StockDTO, Stock>();
            CreateMap<Stock, StockDTO>();
            CreateMap<StockDTO, StockDTO>();
            CreateMap<Stock, Stock>().ForMember(x => x.Silos, opt => opt.Ignore());

            CreateMap<BatchedStockDTO, BatchedStock>();
            CreateMap<BatchedStock, BatchedStockDTO>();
            CreateMap<BatchedStockDTO, BatchedStockDTO>();
            CreateMap<BatchedStock, BatchedStock>().ForMember(x => x.Stock, opt => opt.Ignore()).ForMember(x => x.Silo, opt => opt.Ignore()).ForMember(x => x.Product, opt => opt.Ignore()).IgnoreMembers(x => x.User);

            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, ClientDTO>();
            CreateMap<Client, Client>().ForMember(x => x.ActiveSites, opt => opt.Ignore()).ForMember(x => x.AllSites, opt => opt.Ignore());

            CreateMap<ConsumedStockDTO, ConsumedStock>();
            CreateMap<ConsumedStock, ConsumedStockDTO>();
            CreateMap<ConsumedStockDTO, ConsumedStockDTO>();
            CreateMap<ConsumedStock, ConsumedStock>().ForMember(x => x.Stock, opt => opt.Ignore()).ForMember(x => x.Silo, opt => opt.Ignore());

            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, OrderDTO>();
            CreateMap<Order, Order>().ForMember(x => x.Client, opt => opt.Ignore()).ForMember(x => x.Site, opt => opt.Ignore()).ForMember(x => x.Recipe, opt => opt.Ignore()).ForMember(x => x.ServiceCategory, opt => opt.Ignore());

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, ProductDTO>();
            CreateMap<Product, Product>().ForMember(x => x.BatchedStocks, opt => opt.Ignore()).ForMember(x => x.Client, opt => opt.Ignore()).ForMember(x => x.Driver, opt => opt.Ignore()).ForMember(x => x.Order, opt => opt.Ignore())
                .ForMember(x => x.Recipe, opt => opt.Ignore()).ForMember(x => x.Service, opt => opt.Ignore()).ForMember(x => x.ServiceCategory, opt => opt.Ignore()).ForMember(x => x.Site, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<RecipeDTO, RecipeDTO>();
            CreateMap<Recipe, Recipe>().ForMember(x => x.RecipeContents, opt => opt.Ignore());

            CreateMap<RecipeContentDTO, RecipeContent>();
            CreateMap<RecipeContent, RecipeContentDTO>();
            CreateMap<RecipeContentDTO, RecipeContentDTO>();
            CreateMap<RecipeContent, RecipeContent>().ForMember(x => x.Stock, opt => opt.Ignore()).ForMember(x => x.Recipe, opt => opt.Ignore());

            CreateMap<ServiceDTO, Service>();
            CreateMap<Service, ServiceDTO>();
            CreateMap<ServiceDTO, ServiceDTO>();
            CreateMap<Service, Service>().ForMember(x => x.Driver, opt => opt.Ignore());

            CreateMap<ServiceCategoryDTO, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryDTO>();
            CreateMap<ServiceCategoryDTO, ServiceCategoryDTO>();
            CreateMap<ServiceCategory, ServiceCategory>();

            CreateMap<SiteDTO, Site>();
            CreateMap<Site, SiteDTO>();
            CreateMap<SiteDTO, SiteDTO>();
            CreateMap<Site, Site>().ForMember(x => x.Client, opt => opt.Ignore());

            CreateMap<StockEntryDTO, StockEntry>();
            CreateMap<StockEntry, StockEntryDTO>();
            CreateMap<StockEntryDTO, StockEntryDTO>();
            CreateMap<StockEntry, StockEntry>().ForMember(x => x.Stock, opt => opt.Ignore()).ForMember(x => x.Silo, opt => opt.Ignore()).ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<DriverDTO, Driver>();
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, DriverDTO>();
            CreateMap<Driver, DriverDTO>();
        }
    }
}
