using Pigg.Model;
using System.Collections.Generic;
using System.Globalization;

namespace Pigg.Contracts.Repositories
{
    public interface IContentListItemRepository : IRepository<ContentListItem>, IUnitOfWork
    {
        ContentListItem GetFrontPage(string languageName);

        List<CultureInfo> GetPageLanguages();

        List<CultureInfo> GetPublishedPageLanguages();
    }
}