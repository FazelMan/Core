using Microsoft.EntityFrameworkCore;

namespace FazelMan.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}