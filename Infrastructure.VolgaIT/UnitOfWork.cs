using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Interfaces;
using Infrastructure.VolgaIT.Context;
using Infrastructure.VolgaIT.Repositories;

namespace Infrastructure.VolgaIT
{
    public class UnitOfWork
    {
        private readonly VolgaContext _context;
        public readonly IRepository<User> UserRepository;
        public readonly IRepository<Transport> TransportRepository;
        public readonly IRepository<RentInfo> RentRepository;

        public UnitOfWork(VolgaContext context, UserRepository userRepository, 
            TransportRepository transportRepository, 
            RentRepository rentRepository)
        {  
            _context = context; 
            UserRepository = userRepository;
            TransportRepository = transportRepository;
            RentRepository = rentRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
