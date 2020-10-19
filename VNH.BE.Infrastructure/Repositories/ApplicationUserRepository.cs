using System.Collections.Generic;
using System.Linq;
using VNH.BE.Domain.Aggregates.Identity;

namespace VNH.BE.Infrastructure.Repositories
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAllUser(int pageSize, int pageIndex);
    }
    public class ApplicationUserRepository : GenericRepository<ApplicationUser, string>, IApplicationUserRepository
    {
        private ApplicationDbContext _dbContext;
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ApplicationUser> GetAllUser(int pageSize, int pageIndex)
        {
            return GetAllQueryable().Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }
    }
}
