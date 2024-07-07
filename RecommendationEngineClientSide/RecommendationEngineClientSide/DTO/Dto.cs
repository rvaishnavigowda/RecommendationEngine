using Newtonsoft.Json;
using RecommendationEngineClientSide.ChefDTO;

namespace RecommendationEngineClientSide.DTO
{
    public class LoginRequestDto:SocketResponseDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string UserRole { get; set; }
    }

    public class AddMenuRequestDto
    {
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public decimal MenuPrice { get; set; }
        public DateTime dateCreated { get; set; }
    }

    public class UpdateMenuRequestDto : AddMenuRequestDto
    {
    }

    public class DeleteMenuRequestDto : AddMenuRequestDto
    {
    }

    public class FetchMenuRequestDTO
    {
        public string MenuName { get; set; }
    }

    public class FetchMenuResponseDTO
    {
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public decimal MenuPrice { get; set; }
    }

    public class FetchMenuDTO : SocketResponseDTO
    {
        public IList<FetchMenuResponseDTO> MenuList { get; set; }
    }
    public class NotificationDTO : SocketResponseDTO
    {
        public List<string> Notifications { get; set; }
    }
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
