using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.Emails
{
    public interface IEmailSender
    {
        void Send(MessageDTO message);
    }
}
