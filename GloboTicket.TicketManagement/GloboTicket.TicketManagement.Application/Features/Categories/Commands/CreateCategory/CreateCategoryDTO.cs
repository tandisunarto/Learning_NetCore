using System;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}
