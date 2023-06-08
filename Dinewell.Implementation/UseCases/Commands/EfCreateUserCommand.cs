using Dinewell.Application.Emails;
using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfCreateUserCommand : EfUseCase, ICreateUserCommand
    {
        private CreateUserValidator _validator;
        private IEmailSender _sender;
        public EfCreateUserCommand(DinewellContext context, CreateUserValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 2;

        public string Name => "Create User (EF)";

        public string Description => "Registers user into the database";

        public void Execute(CreateUserDTO request)
        {
            _validator.ValidateAndThrow(request);

            var userRole = Context.Roles.Where(x => x.Name == "User").FirstOrDefault();

            if (userRole == null)
            {
                throw new EntityNotFoundException(1, nameof(Role));
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = hash,
                RoleId = userRole.Id
            };

            if (!string.IsNullOrEmpty(request.Address))
            {
                var newAddress = new UserAddress
                {
                    User = newUser,
                    Name = request.Address,
                    Number = request.AddressNumber
                };
                Context.Users.Add(newUser);
                Context.UserAddresses.Add(newAddress);
            }
            else
            {
                Context.Users.Add(newUser);
            }

            Context.SaveChanges();

            //_sender.Send(new MessageDTO
            //{
            //    To = request.Email,
            //    Title = "Successfull registration!",
            //    Body = "Dear " + request.Username + ", you may login now."
            //});
        }
    }
}
