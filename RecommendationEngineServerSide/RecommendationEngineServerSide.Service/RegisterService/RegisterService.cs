using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> LoginValidation(UserDTO userDTO)
        {
            if (userDTO != null)
            {
                var isUserPresent = await CheckUser(userDTO);
                if (isUserPresent != null)
                {
                    bool isPasswordRight = await CheckPassword(isUserPresent);
                    if (isPasswordRight)
                    {
                        userDTO.UserRole = isUserPresent.UserType.UserTypeName;
                        return userDTO;
                    }
                    else
                    {
                        throw LoginException.WrongPassword();
                    }
                }
                else
                {
                    throw LoginException.NoUserPresent();
                }
            }
            else
            {
                throw CommonException.NullInputException();
            }
        }


        private async Task<User> CheckUser(UserDTO user)
        {
            var isUserPresent= (await  _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName == user.UserName);
            if (isUserPresent != null)
            {
                return isUserPresent;
            }
            else
            {
                return null;
            }
        }

        private async Task<bool> CheckPassword(User user)
        {
            var isPasswordRight = (await _unitOfWork.User.GetById(user.UserId));
            if (isPasswordRight!=null)
            {
                if(isPasswordRight.Password==user.Password)
                {
                    return true;
                }
                else
                { 
                    return false; 
                }
            }
            else
            {
                return false;
            }
        }
    }
}