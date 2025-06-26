namespace PetFamily.Domain.Shared;

public abstract class EntityId<TId> where TId : notnull
{
    public TId Id { get; }
    protected EntityId(TId id) => Id = id;
}