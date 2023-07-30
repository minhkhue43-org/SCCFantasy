using SCCFantasy.Common.Models;
using SCCFantasy.Data.Models;
using SCCFantasy.Data.Repositories;
using SCCFantasy.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
        Task<UserDto> GetUserByName(string userName);
        Task<bool> CheckIfUserNameExist(string userName);
        Task<ResultModel<UserDto>> AddUser(UserDto userDto);
        Task<ResultModel<UserDto>> UpdateUserTeam(string userId, string team);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserByName(string userName)
        {
            var entity = await _userRepository.GetUserByName(userName);

            if (entity == null)
            {
                return null;
            }

            return ToUserDto(entity);
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var entity = await this._userRepository.GetUserById(id);

            if (entity == null) 
            { 
                return null;
            }

            return ToUserDto(entity);
        }

        public async Task<bool> CheckIfUserNameExist(string userName)
        {
            return await _userRepository.CheckIfUserNameExist(userName);
        }

        public async Task<ResultModel<UserDto>> AddUser(UserDto userDto)
        {
           var result = new ResultModel<UserDto>();

           var entity = ToUserEntity(userDto);

            try
            {
                var createdEntity = await _userRepository.AddUser(entity);
                result.Result = ToUserDto(createdEntity);
            }
            catch (Exception ex) 
            {
                result.AddError(ex.Message);
            }

           return result;
        }

        public async Task<ResultModel<UserDto>> UpdateUserTeam(string userId, string team)
        {
            var result = new ResultModel<UserDto>();

            var entity = await _userRepository.GetUserById(userId);
            entity.Team = team;

            try
            {
                var updatedEntity = await _userRepository.UpdateUser(entity);
                result.Result = ToUserDto(updatedEntity);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        private UserDto ToUserDto(UserEntity entity)
        {
            return new UserDto
            { 
                Id = entity.id,
                UserName = entity.UserName,
                Password = entity.Password,
                Roles = entity.Roles,
                Team = entity.Team,
                IsActive = entity.IsActive,
                DateCreated = entity.DateCreated,
                DateUpdated = entity.DateUpdated
            };
        }

        private UserEntity ToUserEntity(UserDto userDto)
        {
            return new UserEntity
            {
                id = userDto.Id,
                UserName = userDto.UserName,
                Password= userDto.Password,
                Roles = userDto.Roles,
                Team = userDto.Team,
                IsActive = userDto.IsActive,
                DateCreated = userDto.DateCreated,
                DateUpdated = userDto.DateUpdated
            };
        }
    }
}
