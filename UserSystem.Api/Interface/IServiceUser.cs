using UserSystem.Api.Dtos;

namespace UserSystem.Api.Interface
{
    public interface IServiceUser
    {
        Task<List<GetAllUsersDto>> GetAllUsersAsync();
        Task<GetUserDetailByIdDto?> GetUserDetailByIdAsync(int id);
        Task<GetUserDetailByIdDto> AddNewUserAsync(string email, string username, string role);
        Task<UpdateUserDetailDto?> UpdateUserDetailAsync(int id, string username, string email, string role);
        Task SoftDeleteAsync(int id);
        Task ReActivateUserAsync(int id);
    }
}