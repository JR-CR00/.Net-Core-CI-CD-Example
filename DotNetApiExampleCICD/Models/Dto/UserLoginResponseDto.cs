using System;

namespace DotNetApiExampleCICD.Models.Dto;

public class UserLoginResponseDto
{
    public UserDataDto? User { get; set; }
    public string ? Token { get; set; }
    public string? Message { get; set; }
}
