using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class RecipeDTO
    {
        private string _isHidden="false";
        private string _gravity="false";

        public int RecipeId { get; set; } = -1;
        public string RecipeCode { get; set; }
        public int RecipeCatNum { get; set; }
        public string RecipeName { get; set; }
        public int Cweg1OrderTime { get; set; }
        public int Cweg2OrderTime { get; set; }
        public int Sweg1OrderTime { get; set; }
        public int Sweg2OrderTime { get; set; }
        public int Kweg1OrderTime { get; set; }
        public int Kweg2OrderTime { get; set; }
        public int MixingTime { get; set; }
        public int FullopenTime { get; set; }
        public int LastopenTime { get; set; }
        public double UnitPrice { get; set; }
        public string CementType { get; set; }
        public string MineralType { get; set; }
        public string AdditiveType { get; set; }
        public string Consistency { get; set; }
        public string Endurance { get; set; }
        public string Dmax { get; set; }
        public string Slump { get; set; }
        public string UnitVolume { get; set; }
        public string Environmental { get; set; }
        public string ChlorideContent { get; set; }
        public string Gravity { get => _gravity; set => _gravity = value.Boolify(); }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); }
        public string EnduranceRate { get; set; }
        public string Fibers { get; set; }
        public string EnduranceDay { get; set; }

    }
}
