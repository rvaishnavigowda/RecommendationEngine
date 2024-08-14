using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.MenuFeedbackRepo
{
    public class MenuFeedbackRepository : GenericRepository<MenuFeedback>, IMenuFeedbackRepository
    {
        public MenuFeedbackRepository(DBContext context) : base(context)
        {
        }
    }
}
