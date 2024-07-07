using AutoMapper;
using RecommendationEngineServerSide.Common.Exceptions;
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
        public async Task AddNotification(string notificationMessage, int notificationType, DateTime notificationDate)
        {
            var notification = new Notification()
            {
                NotificationMessage = notificationMessage,
                NotificationTypeId = notificationType,
                NotificationDate=notificationDate,
                IsDeleted = false
            };
            await _unitOfWork.Notification.Create(notification);
            await _unitOfWork.Save();
        }

        public async Task<List<string>> GetNotification(int userId)
        {
            var isUserPrsent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserId == userId);
            if (isUserPrsent != null)
            {

                var lastNotification = (await _unitOfWork.UserNotification.GetAll()).LastOrDefault(a => a.UserId == userId);
                //var lastNotification = (await _unitOfWork.UserNotification.GetAll())
                //            .Where(a => a.UserId == userId)
                //            .OrderByDescending(a => a.NotificationId)
                //            .FirstOrDefault();
                if (lastNotification != null)
                {
                    List<string> notificationList = new List<string>();
                    var notification=(await _unitOfWork.Notification.GetAll()).Where(a=>a.NotificationId>lastNotification.NotificationId).ToList();
                    if(notification.Any())
                    {
                        
                        foreach (var listItem in notification)
                        {
                            if(isUserPrsent.UserType.UserTypeName.ToLower()=="chef")
                            {
                                if(listItem.NotificationType.NotificationTypeId==1)
                                {
                                    notificationList.Add(listItem.NotificationMessage);
                                    UserNotification userNotification = new UserNotification()
                                    {
                                        UserId = userId,
                                        NotificationId = listItem.NotificationId
                                };
                                    
                                    await _unitOfWork.UserNotification.Add(userNotification);
                                    await _unitOfWork.Save();
                                }
                                
                            }
                            else if(isUserPrsent.UserType.UserTypeName.ToLower() == "employee")
                            {
                                if(listItem.NotificationType.NotificationTypeId==1 || listItem.NotificationType.NotificationTypeId==2)
                                {
                                    notificationList.Add(listItem.NotificationMessage);

                                    UserNotification userNotification = new UserNotification()
                                    {
                                        UserId = userId,
                                        NotificationId = listItem.NotificationId
                                    };
                                    await _unitOfWork.UserNotification.Add(userNotification);
                                    await _unitOfWork.Save();
                                }
                            }
                        }
                        return notificationList;
                    }
                    else
                    {
                        throw CommonException.HandleNullNotification();
                    }
                }
                else
                {
                    throw CommonException.HandleNullNotification();
                }
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
        }

        public async Task<string> GetMenuUpgradeFeedback(string userName)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName == userName);
            if (isUserPresent != null)
            {
                var allType4Notifications = (await _unitOfWork.Notification.GetAll())
                    .Where(n => n.NotificationTypeId == 4)
                    .OrderByDescending(n => n.NotificationDate)
                    .ToList();
                if (allType4Notifications.Any())
                {
                    var latestType4Notification = allType4Notifications.First();
                    var userNotification = (await _unitOfWork.UserNotification.GetAll())
                        .FirstOrDefault(un => un.UserId == isUserPresent.UserId && un.Notification.NotificationTypeId == 4);
                    if(userNotification!=null && userNotification.NotificationId==latestType4Notification.NotificationId)
                    {
                        return null;
                    }
                    else
                    {
                        userNotification = new UserNotification
                        {
                            UserId = isUserPresent.UserId,
                            NotificationId = latestType4Notification.NotificationId
                        };
                        await _unitOfWork.UserNotification.Add(userNotification);
                        await _unitOfWork.Save();
                        return latestType4Notification.NotificationMessage;
                    }                 
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
        }
    }
}