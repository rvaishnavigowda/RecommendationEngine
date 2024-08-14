using RecommendationEngineServerSide.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class EmployeeUpdateDTO : SocketResponseDTO
    {
        public List<ProfileQuestionDTO> ProfileQuestions { get; set; }
    }

    public class ProfileQuestionDTO
    {
        public string Question { get; set; }
        public List<string> ProfileAnswers { get; set; }
    }

    public class UserProfileDetailDTO
    {
        public string userName { get; set; }
        public List<string> userResponse { get; set; }
    }
}

