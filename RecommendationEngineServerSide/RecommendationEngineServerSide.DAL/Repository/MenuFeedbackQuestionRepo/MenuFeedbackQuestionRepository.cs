using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.MenuFeedbackQuestionRepo
{
    public class MenuFeedbackQuestionRepository : GenericRepository<MenuFeedbackQuestion>, IMenuFeedbackQuestionRepository
    {
        public MenuFeedbackQuestionRepository(DBContext context) : base(context)
        {
        }
    }
}
