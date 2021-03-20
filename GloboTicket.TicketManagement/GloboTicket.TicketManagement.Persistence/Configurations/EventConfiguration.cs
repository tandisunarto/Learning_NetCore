using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GloboTicket.TicketManagement.Persistence.Configurations
{
    class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //throw new System.NotImplementedException();
        }
    }
}
