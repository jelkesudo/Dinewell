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
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
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

        public int Id => 1;

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

            string emailBody = "<!DOCTYPE html>" +
            "<html> " +
                                        "<body style='background - color:#ff7f26;text-align:center;'> " +
                                        "<h1 style='color:#051a80;'>Welcome to Dinewell!</h1> " +
                                        "<label style='color: orange; font - size:100px; border: 5px dotted; border - radius:50px'>Dinewell</label> " + "</body> " + "</html>";
            string subject = "Successul registration";
            string mailTitle = "Hello " + request.Username + " and welcome to Dinewell App!";
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("filip.jelic.19.20@ict.edu.rs", "zeV2q3mv");

                MailMessage mailMessage = new MailMessage(new MailAddress("filip.jelic.19.20@ict.edu.rs", mailTitle), new MailAddress(request.Email));
                mailMessage.To.Add(new MailAddress(request.Email));
                mailMessage.Subject = subject;
                mailMessage.Body = emailBody;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
            }

        }
    }
}
