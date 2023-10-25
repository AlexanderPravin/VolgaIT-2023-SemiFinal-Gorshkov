

namespace Domain.VolgaIT.Interfaces
{
    public interface IMapper<Entity, DTO> where Entity : class where DTO : class
    {
        abstract static DTO CreateDTOFromEntity(Entity entity);
        abstract static IEnumerable<DTO> CreateDTOsFromEntities(IEnumerable<Entity> entities);
    }
}
