using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT.Context;

namespace Infrastructure.VolgaIT.Repositories
{
    public class TransportRepository : BaseRepository<Transport>
    {
        public TransportRepository(VolgaContext context) : base(context)
        {
        }
    }
}
