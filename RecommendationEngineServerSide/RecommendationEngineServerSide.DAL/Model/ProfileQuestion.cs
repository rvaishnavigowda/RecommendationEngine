using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class ProfileQuestion
    {
        [Key]
        public int PQId { get; set; }
        public string Question { get; set; }
    }
}
