using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace PiggMvc3App.Models
{
    [Serializable]
    public class PiggIdentity : MarshalByRefObject, IIdentity
    {
        private System.Web.Security.FormsAuthenticationTicket ticket;

        public PiggIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public string AuthenticationType
        {
            get { return "PiggIdentity"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return ticket.UserData; }
        }
    }
}