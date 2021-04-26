using Pigg.Contracts.Repositories;
using Pigg.Model;
using System.Linq;

namespace Pigg.Data.Repositories
{
    public class CustomSettingRepository : RepositoryBase<CustomSetting>, ICustomSettingRepository
    {
        public CustomSettingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public CustomSetting GetSettings(string Key)
        {
            var fp = (DataContext.CustomSettings.FirstOrDefault(p => p.Key == Key));
            return fp;
        }
    }
}