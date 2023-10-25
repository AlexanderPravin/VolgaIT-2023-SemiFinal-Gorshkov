using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.VolgaIT.Repositories
{
    public class UserRepository : BaseRepository<User>
    {

        public UserRepository(VolgaContext context) : base(context)
        {

        }

        public override async Task<User?> GetEntityByIdAsync(string id)
        {
            return await _context.Users
                .Include(x=>x.OwnedTransport)
                .Include(x=>x.RentHistory)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x=>x.Id == Guid.Parse(id));
        }
        public override async Task<User?> GetEntityByAsync(Expression<Func<User, bool>> filter)
        {
            return await _context.Users
                .Include(x => x.OwnedTransport)
                .Include(x => x.RentHistory)
                .AsSplitQuery()
                .FirstOrDefaultAsync(filter);
        }
        public override async Task<ICollection<User>> GetEntitiesByAsync(Expression<Func<User, bool>> filter)
        {
            return await _context.Users
                .Include(x => x.OwnedTransport)
                .Include(x => x.RentHistory)
                .AsSplitQuery()
                .Where(filter)
                .ToListAsync();
        }
    }
}
