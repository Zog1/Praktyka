using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Cryptography;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using CarRental.Services.Models.User;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailServices email;
        private readonly IMapper mapper;
        private readonly ITokenGeneratorService token;
        private readonly IRefreshRepository refreshRepository;
        public AuthorizationService(IUserRepository userRepository,
            IEmailServices email,
            IMapper mapper,
            ITokenGeneratorService token,
            IRefreshRepository refreshRepository)
        {
            this.userRepository = userRepository;
            this.email = email;
            this.mapper = mapper;
            this.token = token;
            this.refreshRepository = refreshRepository;
        }
        /// <summary>
        /// It's function for generate hash password and salt 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var passwordHasher = new PasswordHasher();
            return passwordHasher.GenerateSaltedHash(size, password);         
        }
        /// <summary>
        /// It's function for verify 
        /// input password with hash password
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
        /// <summary>
        /// RegisterUserAsync with correct model 
        /// and with email which doesn't exist in database
        /// save to database and send verification email 
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>return map createUserDto
        /// or return not map createUserDto</returns>
        public async Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto)
        {
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName, createUserDto.IdentificationNumber,
                createUserDto.Email, createUserDto.MobileNumber);
            var check_user = await userRepository.FindByLoginAsync(createUserDto.Email);
            if (check_user == null)
            {
                userRepository.Create(new_user);
                await userRepository.SaveChangesAsync();
                createUserDto.CodeOfVerification = new_user.CodeOfVerification;
                email.EmailAfterRegistration(createUserDto);
            }
            else
                return null;
            return mapper.Map<CreateUserDto>(new_user);
        }
        /// <summary>
        /// Set Password return true if hash generated correct
        /// and Save to database
        /// </summary>
        /// <param name="updateUserPassword"></param>
        /// <returns></returns>
        public async Task<bool> SetPasswordAsync(UpdateUserPasswordDto updateUserPassword)
        {
            var user = await userRepository.FindByCodeOfVerificationAsync(updateUserPassword.CodeOfVerification);
            var saltHashPassword = GenerateSaltedHash(16, updateUserPassword.EncodePassword);
            user.SetPassword(saltHashPassword.Hash, saltHashPassword.Salt);
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Sign in with correct email and input password
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>return TokenDto with new Access Token and Refresh Token
        /// if user null or email or password not correct
        /// return tokenDto with code 401</returns>
        public async Task<TokenDto> SignInAsync(UserLoginDto userLoginDto)
        {
            var user = await userRepository.FindByLoginAsync(userLoginDto.Email);
            TokenDto tokenDto = new TokenDto();
            if (user == null)
            {
                tokenDto.Code = 401;
                return tokenDto;
            }
            if (userLoginDto.Email != user.Email || !VerifyPassword(userLoginDto.Password, user.HashPassword, user.Salt))
            {
                tokenDto.Code = 401;
                return tokenDto;
            }
            //Return two tokens Access, Refresh
            tokenDto.Code = 200;
            tokenDto.AccessToken = token.GenerateToken(user);
            tokenDto.RefreshToken = token.RefreshGenerateToken();
            //Save To database Refresh token 
            RefreshToken refreshToken = new RefreshToken(tokenDto.RefreshToken, user.UserId, true);
            refreshRepository.Create(refreshToken);
            await refreshRepository.SaveChangesAsync();
            return tokenDto;
        }
    }
}
