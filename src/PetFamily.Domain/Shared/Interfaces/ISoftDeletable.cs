namespace PetFamily.Domain.Shared.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    DateTime? DeletionDate { get; }
    public void Delete(bool cascade = true);
    public void Restore(bool cascade = true);
}