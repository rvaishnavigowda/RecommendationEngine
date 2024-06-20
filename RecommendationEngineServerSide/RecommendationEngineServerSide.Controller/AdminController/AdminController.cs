using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Controller.AdminControllers
{
    public class AdminController
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;


        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;

        }

        public async Task<SocketResponseDTO> HandleAddMenu(MenuDTO menuDTO)
        {
            try
            {
                await _adminService.AddMenu(menuDTO);
                ResponseMenuDTO responseMenuDTO = new ResponseMenuDTO()
                {
                    Status = "Sucsess",
                    Message = "The menu has been successfully added."
                };
                return responseMenuDTO;
            }
            catch (LoginException ex)
            {
                return new ResponseMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (CommonException ex)
            {
                return new ResponseMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }

        public async Task<FetchMenuDTO> FetchMenuDetails(FetchMenuRequestDTO fetchMenuRequestDto)
        {
            try
            {
                var menuDetails = await _adminService.GetMenuDetailsByName(fetchMenuRequestDto.MenuName);
                menuDetails.Status = "success";
                menuDetails.Message="The menu items by the name"+fetchMenuRequestDto.MenuName;
                return menuDetails;
            }
            catch(AdminException ex)
            {
                return new FetchMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }

        public async Task<SocketResponseDTO> HandleUpdateMenu(UpdateMenuDTO updateMenuDTO)
        {
            try
            {
                await  _adminService.UpdateMenu(updateMenuDTO);
                SocketResponseDTO responseMenuDTO = new SocketResponseDTO()
                {
                    Status = "Success",
                    Message = "Menu has been succesfully updated."
                };
                return responseMenuDTO;
            }
            catch (LoginException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (CommonException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<MenuListDTO> GetAllMenu()
        {
            try
            {
                var menuList=await _adminService.GetAllMenu();
                menuList.Status = "Success";
                return menuList;
            }
            catch(Exception ex)
            {
                return new MenuListDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> DeleteMenu(DeleteMenuDTO deleteMenuDTO)
        {
            try
            {
                await _adminService.DeleteMenu(deleteMenuDTO);
                SocketResponseDTO responseMenuDTO = new SocketResponseDTO()
                {
                    Status = "Success",
                    Message = "Menu item has been succesfully deleted."
                };
                return responseMenuDTO;
            }
            catch (LoginException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (CommonException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message =  ex.Message
                };
            }
        }

        
    }   
}
