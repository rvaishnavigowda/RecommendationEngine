using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class MenuType
    {
        public int MenuTypeId { get; set; }
        public string MenuTypeName { get; set; }
        public int ISDeleted { get; set; }
    }
}
