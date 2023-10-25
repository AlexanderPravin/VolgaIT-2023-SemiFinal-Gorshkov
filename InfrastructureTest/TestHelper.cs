
using Infrastructure.VolgaIT;
using Infrastructure.VolgaIT.Context;
using Infrastructure.VolgaIT.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTest
{
    public class TestHelper
    {
        private readonly VolgaContext volgaContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<VolgaContext>();
            builder.UseInMemoryDatabase(databaseName: "MeowDB");

            var dbContextOptions = builder.Options;
            volgaContext = new VolgaContext(dbContextOptions);

            volgaContext.Database.EnsureDeleted();
            volgaContext.Database.EnsureCreated();
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(volgaContext, new UserRepository(volgaContext), new TransportRepository(volgaContext), new RentRepository(volgaContext)); }
        }
    }
}
