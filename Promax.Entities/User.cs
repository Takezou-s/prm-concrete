using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class User
    {
        private string _auManAgg = "false";
        private string _auManCem = "false";
        private string _auManAdv = "false";
        private string _isHidden = "false";

        public int UserId { get; set; } = -1;
        public int UserCatNum { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FullName { get; set; }
        public DateTime LastLogin { get; set; }
        public int AuOrders { get; set; }
        public int AuProducts { get; set; }
        public int AuClients { get; set; }
        public int AuRecipes { get; set; }
        public int AuStocks { get; set; }
        public int AuServices { get; set; }
        public string AuManAgg { get => _auManAgg; set => _auManAgg = value.Boolify(); }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); }
        public string AuManCem { get => _auManCem; set => _auManCem = value.Boolify(); }
        public string AuManAdv { get => _auManAdv; set => _auManAdv = value.Boolify(); }
        public UserType UserType { get => (UserType)UserCatNum; set => UserCatNum = (int)value; }
        public AccessRight OrdersAccessRight { get => (AccessRight)AuOrders; set => AuOrders = (int)value; }
        public AccessRight ProductsAccessRight { get => (AccessRight)AuProducts; set => AuProducts = (int)value; }
        public AccessRight ClientsAccessRight { get => (AccessRight)AuClients; set => AuClients = (int)value; }
        public AccessRight RecipesAccessRight { get => (AccessRight)AuRecipes; set => AuRecipes = (int)value; }
        public AccessRight StocksAccessRight { get => (AccessRight)AuStocks; set => AuStocks = (int)value; }
        public AccessRight ServicesAccessRight { get => (AccessRight)AuServices; set => AuServices = (int)value; }
    }
}
