using Pigg.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

namespace Pigg.Data
{
    //class PiggInitializer : DropCreateDatabaseIfModelChanges<PiggDbContext>
    //class PiggInitializer : DropCreateDatabaseAlways<PiggDbContext>
    internal class PiggInitializer : CreateDatabaseIfNotExists<PiggDbContext>
    {
        protected override void Seed(PiggDbContext context)
        {
            var customPages = new List<CustomPage>
                                  {
                                      new CustomPage
                                          {
                                              Title = "Welcome",
                                              LongTitle = "Welcome in this web site",
                                              PageContent = "<p>Welcome in my new web site. I hope that you agree with me that this is an awesome web site.</p><p>&nbsp;</p><p>Admin user</p><p>username: admin<br />password: password</p>",
                                              CultureCode = "en-GB",
                                              IsPublished = true,
                                              IsFrontPage = true,
                                              ShowInList = true,
                                              OrderInList = 0
                                          },
                                          new CustomPage
                                          {
                                              Title = "Benvenuti",
                                              LongTitle = "Benvenuti in questo sito web",
                                              PageContent = "<p>Questo sito web &egrave; basato su Asp.Net MVC e consente di inserire contenuti in pi&ugrave; lingue.</p><p>&nbsp;</p><p>Utente di amministrazione</p><p>username: admin<br />password: password</p><p>E&#39; possibile registrare nuovi utenti e gestirli cliccando sul pulsante in alto a destra</p>",
                                              CultureCode = "it-IT",
                                              IsPublished = true,
                                              IsFrontPage = true,
                                              ShowInList = true,
                                              OrderInList = 0
                                          }
                                  };
            customPages.ForEach(cp => context.CustomPages.Add(cp));

            var webPartPlacements = new List<WebPartPlacement>
                                        {
                                            new WebPartPlacement { WebPartZone = "Left" },
                                            new WebPartPlacement { WebPartZone = "Middle"},
                                            new WebPartPlacement { WebPartZone = "Right"}
                                        };
            webPartPlacements.ForEach(w => context.WebPartPlacements.Add(w));

            var customSetting = new List<CustomSetting>
                                  {
                                      new CustomSetting
                                          {
                                              Key = "Footer",
                                              Title = "Footer Title",
                                              Value ="Footer Value",
                                              Description ="Footer Description",
                                              Reserved = false
                                          }
                                   };
            customSetting.ForEach(cp => context.CustomSettings.Add(cp));
            context.SaveChanges();

            ShopConfig.InitAuthentication();
            SeedAuthentication();
        }

        private static void SeedAuthentication()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("Editor"))
                Roles.CreateRole("Editor");

            if (!Roles.RoleExists("Guest"))
                Roles.CreateRole("Guest");

            if (!WebSecurity.UserExists("admin"))
                WebSecurity.CreateUserAndAccount(
                    "admin",
                    "Pa$$w0rd",
                    new { Email = "admin@admin.com" });

            if (!Roles.GetRolesForUser("admin").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
        }
    }
}