using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string? UserTypeName { get; set; }
        public bool IsDeleted { get; set; }

    }
}
