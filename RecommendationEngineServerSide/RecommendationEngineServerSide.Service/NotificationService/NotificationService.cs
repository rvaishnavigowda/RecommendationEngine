using AutoMapper;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddNotification(string notificationMessage, int notificationType)
        {
            var notification = new Notification()
            {
                NotificationMessage = notificationMessage,
                NotificationTypeId = notificationType,
                IsDeleted = 0
            };
            await _unitOfWork.Notification.Create(notification);
            await _unitOfWork.Save();
        }
    }
}
