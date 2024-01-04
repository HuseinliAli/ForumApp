using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants;
using Common.Events.User;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.RabbitMQ;
using Common.Models.RequestModels;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IMapper mapper,IUserRepository userRepository)
        {
            _mapper=mapper;
            _userRepository=userRepository;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.GetSingleAsync(x => x.EmailAddress ==request.EmailAddress);
            if (existsUser !=null)
                throw new DatabaseValidationException("User is not exists");
            var dbUser = _mapper.Map<Domain.Models.User>(request);
            var rows = await _userRepository.AddAsync(dbUser);

            if (rows>0)
            {
                UserEmailChangeEvent @event = new()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };
                QueueFactory.SendMessage(RabbitMQConstants.UserEchangeName,RabbitMQConstants.DefaultExchangeType,RabbitMQConstants.UserEmailChangedQueueName,@event);
            }

            return dbUser.Id;
        }
    }
}
