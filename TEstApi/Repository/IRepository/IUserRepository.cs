using System;
using TEstApi.Models;
using TEstApi.Models.Dto;

namespace TEstApi.Repository.IRepository;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetUsers();
    ApplicationUser? GetUser(string id);
    bool IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto request);
    Task<UserDataDto> Register(CreateUserDto request);
}
