namespace Autopartner.Task.Infrastructure.DAL.Entities;
public interface IBaseEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset ModifiedAt { get; set; }

}
