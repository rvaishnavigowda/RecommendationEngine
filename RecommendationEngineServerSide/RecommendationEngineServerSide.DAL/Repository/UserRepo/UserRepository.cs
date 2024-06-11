using Microsoft.EntityFrameworkCore;
using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.UserRepo
{
    internal class UserRepository:GenericRepository<User>, IUserRepository
    {
        private readonly DBContext _context;
        protected readonly DbSet<User> _table;
        internal UserRepository(DBContext context) : base(context)
        {
            _context = context;
            _table = _context.Set<User>();
        }

        
    }
}
