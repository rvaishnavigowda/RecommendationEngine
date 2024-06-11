using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;

namespace RecommendationEngineServerSide.DAL.Repository.DailyMenuRepo
{
    public class DailyMenuRepository : GenericRepository<DailyMenu>, IDailyMenuRepository
    {
        public DailyMenuRepository(DBContext context) : base(context)
        {
        }
    }
}
