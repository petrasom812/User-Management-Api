using UserSystem.Api.Models;
using UserSystem.Api.Dtos;
using UserSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace UserSystem.Api.Services
{
    public class ServiceUser
    {
        private readonly AppDbContext _context;
        public ServiceUser(AppDbContext context)
        {
            _context = context;
        }
        //Get All Users
        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.IsActive)
                .Select(u => new GetAllUsersDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
        }
        //Get a user detail by Id
        public async Task<GetUserDetailByIdDto?> GetUserDetailById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return (user == null || !user.IsActive) ? null : new GetUserDetailByIdDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                EditedAt = user.EditedAt
            };
        }

        //Add new user
        public async Task<GetUserDetailByIdDto> AddNewUser(string email, string username, string role)
        {
            await EnsureEmailIsUnique(email);
            var user = new UserModel
            {
                Username = ValidateString(username),
                Email = ValidateString(email),
                IsActive = true,
                Role = ValidateString(role),
                CreatedAt = DateTime.UtcNow,
                EditedAt = null
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new GetUserDetailByIdDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                EditedAt = user.EditedAt
            };
        }
        //Update user
        public async Task<UpdateUserDetailDto?> UpdateUserDetail(int id, string username, string email, string role)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || !user.IsActive)
                return null;

            if (user.Email != email)
            {
                await EnsureEmailIsUniqueForUpdate(id, email);
            }

            user.Username = ValidateString(username);
            user.Email = ValidateString(email);
            user.Role = ValidateString(role);
            user.EditedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UpdateUserDetailDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                EditedAt = user.EditedAt
            };
        }
        public async Task SoftDelete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new ArgumentException("User not found");
            if (!user.IsActive)
                throw new ArgumentException("User already inactive");
            user.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public string ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value can not be null or white space.");
            return value;
        }

        public async Task EnsureEmailIsUnique(string email)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email);
            if (exists)
                throw new ArgumentException("Email already exists");
        }

        public async Task EnsureEmailIsUniqueForUpdate(int id, string email)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);
            if (exists)
                throw new ArgumentException("Email already exists");
        }
    }
}