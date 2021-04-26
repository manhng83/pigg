using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Security;

namespace PiggMvc3App.Models.UserAdministration
{
    public class IndexViewModel
    {
        public IPagedList<MembershipUser> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}