using Pigg.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Pigg.CQRS.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<CustomPage> GetAll();

        IEnumerable<CustomPage> GetRootElements(string cultureCode);

        CustomPage GetFrontPage(string cultureCode);

        List<CultureInfo> GetCultures();

        List<CultureInfo> GetExistentCultures();

        CustomPage GetPage(int customPageId);

        CustomPage GetPage(Guid entityId);
    }
}