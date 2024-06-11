using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.FeedbackRepo
{
    internal class FeedbackRepository: GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DBContext context) : base(context)
        {
        }
    }
}
