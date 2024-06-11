using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class UserOrder
    {
        [Key]
        public int UserOrderId { get; set; }
        //public DateTime OrderDate { get; set; }
        //public int UserId { get; set; }
        //public virtual User? User { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int DailyMenuId { get; set; }
        public virtual DailyMenu? DailyMenu { get; set; }


    }
}
