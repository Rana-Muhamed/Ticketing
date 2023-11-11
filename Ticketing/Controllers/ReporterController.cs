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
    [Authorize(Roles = "Reporter")]
    public class ReporterController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public ReporterController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (int.TryParse(SearchValue, out int searchIntValue)) {
            var tickets = _unitofWork.TicketRepo.SearchTicketsById(searchIntValue);
                var mappedProduct = _mapper.Map<IEnumerable<Tickets>, IEnumerable<TicketsViewModel>>(tickets);
                return View(mappedProduct);
            } 
            else {
            var tickets = await _unitofWork.TicketRepo.GetAll();
            var mappedTickets = _mapper.Map<IEnumerable<Tickets>, IEnumerable<TicketsViewModel>>(tickets);
            return View(mappedTickets);
        }
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tickets tickets)
        {
           
            if (ModelState.IsValid)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(tickets.CreatorId))
                {
                    tickets.CreatorId = currentUserId;
                }
                tickets.LastUpdatedById = currentUserId;

                await _unitofWork.TicketRepo.Add(tickets);
                await _unitofWork.Complete();
                return RedirectToAction("Index");
            }

            return View();
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

    }
}
