using Ticketing.Models;

namespace Ticketing.Interfaces
{
    public interface ITicketRepo
    {
        Task<IEnumerable<Tickets>> GetAll();
        Task<Tickets> Get(int id);
        Task Add(Tickets item);
        void Update(Tickets item);
        void Delete(Tickets item);
        IQueryable<Tickets> SearchTicketsById(int ticketId);
        Task<IEnumerable<Tickets>> FilterNewAndOrderSLEndDate();
        Task UpdateUserRoleToTechnician(string userId, int ticketId);
        Task<IEnumerable<Tickets>> GetFilteredAndOrderedTicketsForTechnician(string technicianId);
        Task UpdateStatusAndAddCommentAsync(int ticketId, string newStatus, string comment);

    }
}
