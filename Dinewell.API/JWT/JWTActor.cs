using Dinewell.Application;
using System.Collections.Generic;

namespace Dinewell.API.JWT
{
    public class JWTActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }
    }
}
