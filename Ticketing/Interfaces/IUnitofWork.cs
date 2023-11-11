namespace Ticketing.Interfaces
{
    public interface IUnitofWork:IDisposable
    {
        public ITicketRepo TicketRepo { get; set; }
        Task<int> Complete();
    }
}
