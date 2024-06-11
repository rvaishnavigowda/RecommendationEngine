using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class DailyMenu
    {
        public int DailyMenuId { get; set; }
        public int DailyMenuName { get; set; }
        public DateTime DailyMenuDate { get; set; }

        public int MenuId { get; set; }
        public virtual Menu? Menu { get; set; }

        public int ISDeleted { get; set; }
    }
}
