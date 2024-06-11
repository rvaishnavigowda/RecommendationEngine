using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.MenuRepo
{
    internal class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DBContext context) : base(context)
        {
        }
    }
}
