using WebMatrix.WebData;

namespace Pigg.Data
{
    public static class ShopConfig
    {
        public static void InitAuthentication()
        {
            WebSecurity.InitializeDatabaseConnection("PiggDbContext", "People", "Id", "Email", autoCreateTables: true);
        }
    }
}