using SCCFantasy.Common.Models;
using SCCFantasy.Common.Security;
using SCCFantasy.Services;
using SCCFantasy.Services.Models;
using SCCFantasy.Web.Models;

namespace SCCFantasy.Web.Services
{
    public interface IUserWebService
    {
        Task<UserModel> GetUserModel(string id);
        Task<ResultModel<UserDto>> Login(LoginViewModel loginViewModel);
        Task<ResultModel<UserDto>> Register(RegisterViewModel registerViewModel);
        Task<ResultModel<UserDto>> UpdateUserTeam(string userId, string team);
    }

    public class UserWebService : IUserWebService
    {
        private readonly IUserService _userService;

        public UserWebService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultModel<UserDto>> UpdateUserTeam(string userId, string team)
        {
            return await _userService.UpdateUserTeam(userId, team); 
        }

        public async Task<ResultModel<UserDto>> Register(RegisterViewModel registerViewModel)
        {
            var result = new ResultModel<UserDto>();

            var ifUserNameExist = await _userService.CheckIfUserNameExist(registerViewModel.UserName);

            if (ifUserNameExist)
            {
                result.AddError("UserName already exists");
                return result;
            }

            var userDto = new UserDto
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerViewModel.UserName,
                Password = HashDataHelper.HashData(registerViewModel.Password),
                IsActive = true,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Roles = "User"
            };

            return await _userService.AddUser(userDto);
        }

        public async Task<ResultModel<UserDto>> Login(LoginViewModel loginViewModel)
        {
            var result = new ResultModel<UserDto>();

            var user = await _userService.GetUserByName(loginViewModel.UserName);

            if (user == null)
            {
                result.AddError("UserName and Password do not match");
                return result;
            }

            var comparePassword = HashDataHelper.HashData(loginViewModel.Password) == user.Password;

            if (!comparePassword)
            {
                result.AddError("UserName and Password do not match"); 
                return result;
            }

            result.Result = user;

            return result;
        }

        public async Task<UserModel> GetUserModel(string id)
        {
            var userDto = await _userService.GetUserById(id);
            if (userDto == null) { return  null; }

            var userModel = ToUserModel(userDto);

            var team = userDto.Team;

            if (!string.IsNullOrEmpty(team))
            {
                userModel.Team = team.Split(';').Select(x => int.Parse(x)).ToArray();
            }
            else
            {
                userModel.Team = Array.Empty<int>();
            }

            return userModel;
        }

        private UserModel ToUserModel(UserDto dto)
        {
            return new UserModel 
            { 
                Id = dto.Id,
                UserName = dto.UserName,
                Roles = dto.Roles,
                IsActive = dto.IsActive
            };
        }
    }
}
