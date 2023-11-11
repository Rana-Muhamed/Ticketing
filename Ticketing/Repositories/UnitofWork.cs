using Ticketing.Data;
using Ticketing.Interfaces;

namespace Ticketing.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _dbContext;
        public ITicketRepo TicketRepo { get ; set; }
        public UnitofWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            TicketRepo = new TicketRepo(dbContext);
        }

        async Task<int> IUnitofWork.Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
