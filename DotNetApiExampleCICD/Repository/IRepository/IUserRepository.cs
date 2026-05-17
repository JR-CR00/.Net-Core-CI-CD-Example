using System;
using DotNetApiExampleCICD.Models;
using DotNetApiExampleCICD.Models.Dto;

namespace DotNetApiExampleCICD.Repository.IRepository;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetUsers();
    ApplicationUser? GetUser(string id);
    bool IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto request);
    Task<UserDataDto> Register(CreateUserDto request);
}
