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

}
