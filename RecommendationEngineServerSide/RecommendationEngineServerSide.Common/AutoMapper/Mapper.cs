using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.AutoMapper
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, ResponseUserDTO>();
            CreateMap<MenuDTO, ResponseMenuDTO>();
        }
        
    }
}
