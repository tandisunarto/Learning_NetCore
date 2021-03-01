using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVM>
    {
        private IMapper mapper;
        private IAsyncRepository<Event> eventRepository;
        private IAsyncRepository<Category> categoryRepository;

        public GetEventDetailQueryHandler(
            IMapper mapper,
            IAsyncRepository<Event> eventRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<EventDetailVM> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await eventRepository.GetByIdAsync(request.Id);
            var eventDetailDTO = mapper.Map<EventDetailVM>(@event);

            var category = await categoryRepository.GetByIdAsync(@event.CategoryId);

            eventDetailDTO.Category = mapper.Map<CategoryDTO>(category);

            return eventDetailDTO; 
        }
    }
}
