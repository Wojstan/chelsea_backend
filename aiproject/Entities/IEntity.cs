using aiproject.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aiproject.Repositories
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}