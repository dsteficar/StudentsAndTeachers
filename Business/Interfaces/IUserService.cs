using Data.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByID(int id);
        Task UpdateUser(int id, UserDTO userDTO);
        Task DeleteUser(int id);
        Task<bool> AddUser(UserDTO userDTO);
    }
}
