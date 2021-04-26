using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PiggMvc3App.Models.UserAdministration
{
    public class RoleViewModel
    {
        public string Role { get; set; }
        public IEnumerable<MembershipUser> Users { get; set; }
    }
}