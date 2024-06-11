using AutoMapper;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.EmplyoeeService
{
    public class EmplyoeeService : IEmplyoeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmplyoeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task GetDailyMenuList()
        {

        }
    }
}
