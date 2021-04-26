using Pigg.Model;
using System.Collections.Generic;
using System.Globalization;

namespace Pigg.Contracts.Repositories
{
    public interface ICustomPageRepository : IRepository<CustomPage>, IUnitOfWork
    {
        IEnumerable<CustomPage> GetRootElements(string languageName);

        CustomPage GetFrontPage(string languageName);

        List<CultureInfo> GetPageLanguages();

        List<CultureInfo> GetPublishedPageLanguages();
    }
}