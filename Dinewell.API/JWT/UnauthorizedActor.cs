using Dinewell.Application;
using System.Collections.Generic;

namespace Dinewell.API.JWT
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "";

        public string Username => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> {2};
    }
}
