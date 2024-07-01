using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int MenuTypeId { get; set; }
        public string? MenuName { get; set; }
        public decimal Price { get; set; }

        public DateTime MenuCreationDate { get; set; }
        public virtual MenuType? MenuType { get; set; }

        public int MenuStatus { get; set; }
    }
}
