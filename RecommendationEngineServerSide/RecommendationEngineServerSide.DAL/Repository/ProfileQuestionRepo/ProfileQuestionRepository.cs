using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.ProfileQuestionRepo
{
    public class ProfileQuestionRepository :GenericRepository<ProfileQuestion>, IProfileQuestionRepository
    {
        public ProfileQuestionRepository(DBContext context) : base(context)
        {

        }
    }
}
