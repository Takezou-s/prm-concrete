using AutoMapper;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ManuelSession, ManuelSession>();
            CreateMap<ManuelSession, ManuelSessionDto>();
            CreateMap<ManuelSessionDto, ManuelSessionDto>();
            CreateMap<ManuelSessionDto, ManuelSession>();

            CreateMap<Consumed, Consumed>().IgnoreMembers(x => x.Silo, x => x.Stock);
            CreateMap<Consumed, ConsumedDto>();
            CreateMap<ConsumedDto, ConsumedDto>();
            CreateMap<ConsumedDto, Consumed>();

            CreateMap<Batched, Batched>().IgnoreMembers(x => x.Silo, x => x.Stock);
            CreateMap<Batched, BatchedDto>();
            CreateMap<BatchedDto, BatchedDto>();
            CreateMap<BatchedDto, Batched>();

            CreateMap<Production, Production>().IgnoreMembers(x => x.Recipe);
            CreateMap<Production, ProductionDto>();
            CreateMap<ProductionDto, ProductionDto>();
            CreateMap<ProductionDto, Production>();

            CreateMap<Recipe, Recipe>().IgnoreMembers(x => x.RecipeContents);
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();

            CreateMap<RecipeContent, RecipeContent>().IgnoreMembers(x => x.Recipe, x => x.Silo);
            CreateMap<RecipeContent, RecipeContentDto>();
            CreateMap<RecipeContentDto, RecipeContentDto>();
            CreateMap<RecipeContentDto, RecipeContent>();

            CreateMap<RecipeObject, RecipeObject>();
            CreateMap<RecipeObject, RecipeObjectDto>();
            CreateMap<RecipeObjectDto, RecipeObjectDto>();
            CreateMap<RecipeObjectDto, RecipeObject>();

            CreateMap<Silo, Silo>().IgnoreMembers(x => x.Stock);
            CreateMap<Silo, SiloDto>();
            CreateMap<SiloDto, SiloDto>();
            CreateMap<SiloDto, Silo>();

            CreateMap<Stock, Stock>().IgnoreMembers(x => x.Silos);
            CreateMap<Stock, StockDto>();
            CreateMap<StockDto, StockDto>();
            CreateMap<StockDto, Stock>();

            CreateMap<Settings, Settings>();
            CreateMap<Settings, SettingsDto>();
            CreateMap<SettingsDto, SettingsDto>();
            CreateMap<SettingsDto, Settings>();

            CreateMap<StockEntry, StockEntry>().IgnoreMembers(x => x.Silo, x => x.Stock);
            CreateMap<StockEntry, StockEntryDto>();
            CreateMap<StockEntryDto, StockEntryDto>();
            CreateMap<StockEntryDto, StockEntry>();

            CreateMap<StockEntryView, StockEntry>();
            CreateMap<StockEntry, StockEntryView>();

            CreateMap<User, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
