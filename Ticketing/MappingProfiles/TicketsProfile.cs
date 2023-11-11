using AutoMapper;
using Ticketing.Models;
using Ticketing.ViewModels;

namespace Ticketing.MappingProfiles
{
    public class TicketsProfile:Profile
    {
        public TicketsProfile()
        {
            CreateMap<TicketsViewModel, Tickets>().ReverseMap();
        }
    }
}
