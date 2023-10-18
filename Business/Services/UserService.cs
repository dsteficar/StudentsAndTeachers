using AutoMapper;
using Business.CustomExceptions;
using Business.ExtensionMethods;
using Business.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            if (!user.CheckUserData())
                return false;

            await _userRepository.Insert(user);

            return true;
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users.Select(x => _mapper.Map<UserDTO>(x)).ToList();
        }

        public async Task<UserDTO> GetUserByID(int id)
        {
            var user = await _userRepository.GetById(id);
            var userDTOMapped = _mapper.Map<UserDTO>(user);

            return userDTOMapped;
        }

        public async Task UpdateUser(int id, UserDTO userDTO)
        {
            var userDb = await _userRepository.GetById(id);

            if (userDb == null)
            {
                throw new UserNotFoundException();
            }

            _mapper.Map(userDTO, userDb);

            try
            {
                await _userRepository.Update(userDb);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }
    }
}
