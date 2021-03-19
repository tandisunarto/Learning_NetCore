using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private IMapper mapper;
        private IEventRepository eventRepository;
        private IEmailService emailService;

        public CreateEventCommandHandler(
            IMapper mapper,
            IEventRepository eventRepository, 
            IEmailService emailService)
        {
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var @event = mapper.Map<Event>(request);
            @event = await eventRepository.AddAsync(@event);

            var email = new Email
            {
                To = "tandi.sunarto@hotmail.com",
                Subject = $"A new event was created: {request}",
                Body = $"A new event was created: {request}"
            };

            try
            {
                await emailService.SendEmail(email);
            }
            catch (Exception)
            {
                 throw;
            }

            return @event.EventId;
        }
    }
}
