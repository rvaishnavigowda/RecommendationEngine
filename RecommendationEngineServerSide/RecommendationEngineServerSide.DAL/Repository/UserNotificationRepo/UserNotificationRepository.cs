using Microsoft.EntityFrameworkCore;
using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.UserNotificationRepo
{
    public class UserNotificationRepository :GenericRepository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(DBContext context) : base(context)
        { }

    }
}
