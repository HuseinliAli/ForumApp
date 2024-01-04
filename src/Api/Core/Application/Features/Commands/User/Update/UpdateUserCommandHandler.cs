using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants;
using Common.Events.User;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.RabbitMQ;
using Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);
        
            if (dbUser == null)
                throw new DatabaseValidationException("User not found");
            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress)!=0;
            _mapper.Map(request, dbUser);
            var rows = await _userRepository.UpdateAsync(dbUser);

            if (rows>0 &&emailChanged)
            {
                UserEmailChangeEvent @event = new()
                {
                    OldEmailAddress = dbEmailAddress,
                    NewEmailAddress = dbUser.EmailAddress
                };
                QueueFactory.SendMessage(RabbitMQConstants.UserEchangeName, RabbitMQConstants.DefaultExchangeType, RabbitMQConstants.UserEmailChangedQueueName, @event);
                dbUser.EmailConfirmed = false;
                await _userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
