using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheaterApplication.Bll.Exceptions;
using TheaterApplication.Bll.Helpers.Interfaces;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Models.Enums;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Bll.Storages.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Enums;
using TheaterApplication.Dal.Repositories.Interfaces;
using TheaterApplication.Utils.Settings;

namespace TheaterApplication.Bll.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly IApplicationStorage _appStorage;

        private readonly TokenSettings _tokenSettings;

        public UserService(IUserRepository userRepository, 
            ITokenRepository tokenRepository,
            IUserRoleRepository userRoleRepository,
            IPasswordHelper passwordHelper,
            ITokenHelper tokenHelper,
            IMapper mapper,
            IApplicationStorage appStorage,

            TokenSettings tokenSettings)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHelper = passwordHelper;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _appStorage = appStorage;

            _tokenSettings = tokenSettings;
        }

        public async Task<UserWithTokenData> LoginAsync(string email, string password)
        {
            password = _passwordHelper.Encrypt(password);
            var userDb = await _userRepository.FindByEmailAsync(email);

            if(userDb == null || userDb.Password != password || !userDb.Approved.HasValue)
            {
                throw new InternalHandlingException("UL.01", 
                    "Email or password is incorrect");
            }

            var userWithTokenData = _mapper.Map<UserWithTokenData>(userDb);
            userWithTokenData = await CreateTokenAsync(userWithTokenData);

            return userWithTokenData;
        }

        public async Task<User> CreateAsync(string email, string password)
        {
            password = _passwordHelper.Encrypt(password);
            var userDb = await _userRepository.FindByEmailAsync(email);
            var approveCode = GenerateApproceCode();

            if (userDb == null)
            {
                userDb = new UserDbModel();
                userDb.Email = email;
                userDb.Password = password;
                userDb.ApproveCode = approveCode;

                await _userRepository.InsertAsync(userDb);

                var userRoleDb = new UserRoleDbModel();
                userRoleDb.UserId = userDb.Id;
                userRoleDb.RoleId = (byte)RoleEnum.Client;

                await _userRoleRepository.InsertAsync(userRoleDb);
            }
            else
            {
                if (userDb.Approved.HasValue)
                {
                    throw new InternalHandlingException("UC.01",
                    "User already registered");
                }
                else
                {
                    userDb.ApproveCode = approveCode;

                    await _userRepository.UpdateAsync(userDb);
                }
            }

            //TODO: send email

            var result = _mapper.Map<User>(userDb);

            return result;
        }

        public async Task<UserWithTokenData> ApproveAsync(string email, Guid? code)
        {
            var userDb = await _userRepository.FindByEmailAsync(email);

            if(userDb == null)
            {
                throw new InternalHandlingException("UA.01",
                    "User not found");
            }

            if (userDb.Approved.HasValue)
            {
                throw new InternalHandlingException("UA.02",
                    "User already approved");
            }

            if(userDb.ApproveCode != code)
            {
                throw new InternalHandlingException("UA.03",
                    "Code not valid");
            }

            userDb.Approved = DateTime.UtcNow;

            await _userRepository.UpdateAsync(userDb);

            var userWithTokenData = _mapper.Map<UserWithTokenData>(userDb);
            userWithTokenData = await CreateTokenAsync(userWithTokenData);

            return userWithTokenData;
        }

        public AuthorizationResultEnum Authorization(string token, string[] roles)
        {
            var userWithToken = string.IsNullOrEmpty(token)
                ? null : _tokenHelper.DecryptData(token);

            AuthorizationResultEnum result;

            if (userWithToken == null)
            {
                result = AuthorizationResultEnum.UserNotFound;
            }
            else if(userWithToken.Expired.HasValue && userWithToken.Expired <= DateTime.UtcNow)
            {
                result = AuthorizationResultEnum.TokenExpired;
            }
            else if (roles != null && !userWithToken.Roles.Intersect(roles).Any())
            {
                result = AuthorizationResultEnum.RoleNotAllowed;
            }
            else
            {
                _appStorage.User = userWithToken;
                result = AuthorizationResultEnum.OK;
            }

            return result;
        }

        private async Task<UserWithTokenData> CreateTokenAsync(
            UserWithTokenData userWithTokenData)
        {
            string token = _tokenHelper.EncyptData(userWithTokenData);
            var expired = DateTime.UtcNow.AddMinutes(_tokenSettings.LifeTimeMinutes);

            var tokenDb = new TokenDbModel();
            tokenDb.UserId = userWithTokenData.Id;
            tokenDb.Expired = expired;
            tokenDb.Token = token;

            await _tokenRepository.InsertAsync(tokenDb);

            userWithTokenData.Token = token;
            userWithTokenData.Expired = expired;

            return userWithTokenData;
        }

        private Guid GenerateApproceCode()
        {
            // TODO uncomment
            //var result = Guid.NewGuid();
            var result = Guid.Parse("{05A06DCE-3B32-40E6-9F1B-92794687AE3F}");

            return result;
        }
    }
}
