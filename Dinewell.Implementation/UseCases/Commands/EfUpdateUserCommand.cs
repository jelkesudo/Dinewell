using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private UpdateUserValidator _validator;
        public EfUpdateUserCommand(DinewellContext context, UpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 27;

        public string Name => "Update user (EF)";

        public string Description => "";

        public void Execute(UpdateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(User));
            }

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                user.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                user.LastName = request.LastName;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = request.Password;
            }

            if (!string.IsNullOrEmpty(request.Username))
            {
                user.Username = request.Username;
            }

            if (!string.IsNullOrEmpty(request.Address))
            {
                var getAddress = Context.UserAddresses.Where(x => x.UserId == request.Id).FirstOrDefault();
                if (getAddress == null)
                {
                    var newAddress = new UserAddress
                    {
                        User = user,
                        Name = request.Address,
                        Number = request.AddressNumber
                    };
                    Context.UserAddresses.Add(newAddress);
                }
                else
                {
                    getAddress.Name = request.Address;
                    getAddress.Number = request.AddressNumber;
                    Context.Entry(getAddress).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
