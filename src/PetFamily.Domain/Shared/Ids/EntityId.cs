namespace PetFamily.Domain.Shared.Ids;

public abstract class EntityId<TId>(TId id)
    where TId : notnull
{
    public TId Id { get; } = id;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (EntityId<TId>)obj;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(EntityId<TId>? left, EntityId<TId>? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(EntityId<TId>? left, EntityId<TId>? right)
    {
        return !(left == right);
    }
}