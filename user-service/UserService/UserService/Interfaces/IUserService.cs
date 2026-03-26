using UserService.DTOs;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAll();

        Task<UserResponseDto?> GetById(int id);


        Task<List<UserResponseDto>> GetChauffeurs();

        Task<List<UserResponseDto>> GetControleurs();

        Task<UserResponseDto> Create(CreateUserDto dto);

        Task<UserResponseDto> Update( int id, UpdateUserDto dto);

        Task<bool> Delete(int id);
    }
}
