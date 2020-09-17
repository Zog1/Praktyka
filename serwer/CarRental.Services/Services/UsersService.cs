using AutoMapper;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UsersService(
          IUserRepository userRepository,
          IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Change flag IsDeleted in the database from false to true.  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return true or false</returns>
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await userRepository.FindByIdAsync(id);
            if (user == null) { return false; }
            user.Delete(true);
            await userRepository.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Get all users from database.
        /// </summary>
        /// <returns>Returns list with users mapped to UserDto. </returns>
        public async Task<IEnumerable<UsersDto>> GetAllUsersAsync()
        {
            var all_users = await userRepository.FindAllUsersAsync();
            return mapper.Map<IEnumerable<UsersDto>>(all_users);
        }
        /// <summary>
        /// Get user by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns user mapped to UserDto.</returns>
        public async Task<UsersDto> GetUserAsync(int id)
        {
            var user = await userRepository.FindByIdDetailsAsync(id);
            return mapper.Map<UsersDto>(user);
        }
        /// <summary>
        /// Change one or many properties of user in the database.
        /// </summary>
        /// <param name="usersDto"></param>
        /// <returns>Returns user mapped to UserDto.</returns>
        public async Task<UsersDto> UpdateUserAsync(UsersDto usersDto)
        {
            var user = await userRepository.FindByIdDetailsAsync(usersDto.UserId);
            if (user == null)
            {
                usersDto.isValid = false;
                return usersDto;
            }
            if (usersDto.Email == user.Email)
            {
                usersDto.isValid = true;
                user.Update(usersDto.FirstName, usersDto.LastName, usersDto.IdentificationNumber, usersDto.Email, usersDto.MobileNumber);
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();
                return usersDto;
            }
            else
                return mapper.Map<UsersDto>(user);
        }
    }
}
