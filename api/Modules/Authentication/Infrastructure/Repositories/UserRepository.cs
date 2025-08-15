using Api.Modules.Authentication.Domain;
using Api.Shared.DB;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Authentication.Infrastructure.Repositories
{
    public class UserRepository(ApiDbContext context)
    {
        private readonly ApiDbContext _context = context;
        public async Task Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetOne(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
