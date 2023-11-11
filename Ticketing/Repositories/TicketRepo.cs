using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Ticketing.Data;
using Ticketing.Interfaces;
using Ticketing.Models;

namespace Ticketing.Repositories
{
    public class TicketRepo : ITicketRepo
    {
        private protected readonly ApplicationDbContext _dbContext;
        public TicketRepo(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task Add(Tickets item)

         => await _dbContext.AddAsync(item);

        public void Delete(Tickets item)

         => _dbContext.Remove(item);

        public async Task<Tickets> Get(int id)

         => await _dbContext.Set<Tickets>().FindAsync(id);
        

        public async Task<IEnumerable<Tickets>> GetAll() { 

         return await _dbContext.Tickets
                .Include(P => P.Creator)
                .Include(P => P.LastUpdatedBy)
                .Include(P => P.Technician)
                .ToListAsync();
        }
        public void Update(Tickets item)
             => _dbContext.Update(item);

        public IQueryable<Tickets> SearchTicketsById(int TicketId)

       => _dbContext.Tickets.Where(P => P.Id == TicketId);
        public async Task<IEnumerable<Tickets>> FilterNewAndOrderSLEndDate()
        {
            //await _dbContext.Set<Tickets>().Where(P => P.Status == "New")
            //.OrderBy(p => p.SLEndDateTime).ToListAsync();
            var newTickets = await _dbContext.Set<Tickets>()
                .Include(P => P.Creator)
                .Include(P => P.LastUpdatedBy)
                .Include(P => P.Technician)
        .Where(p => p.Status == "New")
        .ToListAsync(); 
            var orderedTickets = newTickets
                .OrderBy(p => p.SLEndDateTime);

            return orderedTickets.ToList();
        }

        public async Task UpdateUserRoleToTechnician(string userId, int ticketId)
        {
            // Query to get the role Id for "Technician"
            var technicianRoleId = await _dbContext.Roles
                .Where(r => r.Name == "Technician")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (technicianRoleId != null)
            {
                // Remove existing roles (if any)
                await _dbContext.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .ForEachAsync(ur => _dbContext.UserRoles.Remove(ur));

                // Add the "Technician" role
                _dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = technicianRoleId
                });
                // Update the TechnicianId property of the specified ticket
                var ticket = await _dbContext.Tickets.FindAsync(ticketId);
                if (ticket != null)
                {
                    ticket.TechnicianId = userId;
                    _dbContext.Update(ticket);
                }

                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Tickets>> GetFilteredAndOrderedTicketsForTechnician(string technicianId)
        {
            var filteredTickets = await _dbContext.Tickets
       .Include(t => t.Creator)
       .Include(t => t.LastUpdatedBy)
       .Include(t => t.Technician)
       .Where(t => t.Status == "New" && t.TechnicianId == technicianId)
       .ToListAsync();

            var orderedTickets = filteredTickets
                .OrderBy(t => t.SLEndDateTime);

            return orderedTickets;
        }

        public async Task UpdateStatusAndAddCommentAsync(int ticketId, string newStatus, string comment)
        {
            var ticket = await _dbContext.Tickets.FindAsync(ticketId);

            if (ticket != null)
            {
                ticket.Status = newStatus;
                ticket.LastActionAt = DateTime.Now;

                if (!string.IsNullOrEmpty(comment))

                    ticket.Comment = comment;
                }

                _dbContext.Update(ticket);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

