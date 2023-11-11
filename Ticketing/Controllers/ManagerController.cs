using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Ticketing.Interfaces;
using Ticketing.Models;
using Ticketing.Repositories;
using Ticketing.ViewModels;

namespace Ticketing.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public ManagerController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (int.TryParse(SearchValue, out int searchIntValue))
            {
                var tickets = _unitofWork.TicketRepo.SearchTicketsById(searchIntValue);
                var mappedProduct = _mapper.Map<IEnumerable<Tickets>, IEnumerable<TicketsViewModel>>(tickets);
                return View(mappedProduct);
            }
            else
            {
                var tickets = await _unitofWork.TicketRepo.FilterNewAndOrderSLEndDate();
                var mappedTickets = _mapper.Map<IEnumerable<Tickets>, IEnumerable<TicketsViewModel>>(tickets);
                return View(mappedTickets);
            }
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var ticket = await _unitofWork.TicketRepo.Get(id.Value);
            if (ticket == null)
                return NotFound();
            var mappedProduct = _mapper.Map<Tickets, TicketsViewModel>(ticket);

            return View(viewName, mappedProduct);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, TicketsViewModel TicketVM)

        {
            if (id != TicketVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (string.IsNullOrEmpty(TicketVM.CreatorId))
                    {
                        TicketVM.CreatorId = currentUserId;
                    }
                    TicketVM.LastUpdatedById = currentUserId;
                    if (!string.IsNullOrEmpty(TicketVM.TechnicianId))
                    {
                        await _unitofWork.TicketRepo.UpdateUserRoleToTechnician(TicketVM.TechnicianId, TicketVM.Id);
                    }
                    var mappedTicket = _mapper.Map<TicketsViewModel, Tickets>(TicketVM);
                    _unitofWork.TicketRepo.Update(mappedTicket);

                    await _unitofWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(TicketVM);
        }
    }
}
