using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT.Context;

namespace Infrastructure.VolgaIT.Repositories
{
    public class RentRepository : BaseRepository<RentInfo>
    {
        public RentRepository(VolgaContext context) : base(context)
        {
        }
    }
}
