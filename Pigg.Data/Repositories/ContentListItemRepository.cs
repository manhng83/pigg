using Pigg.Contracts.Repositories;
using Pigg.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pigg.Data.Repositories
{
    public class ContentListItemRepository : RepositoryBase<ContentListItem>, IContentListItemRepository
    {
        public ContentListItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public ContentListItem GetFrontPage(string languageName)
        {
            try
            {
                var fp = (DataContext.ContentListItems.FirstOrDefault(p => p.IsPublished && p.IsFrontPage && p.LanguageIsoCode == languageName) ??
                                 DataContext.ContentListItems.FirstOrDefault(p => p.IsPublished && p.LanguageIsoCode == languageName)) ??
                                DataContext.ContentListItems.FirstOrDefault(p => p.LanguageIsoCode == languageName);
                return fp;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("There are not visible item for the selected language");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public List<System.Globalization.CultureInfo> GetPageLanguages()
        {
            var items = GetAll().GroupBy(cp => cp.LanguageIsoCode);
            return items.Select(item => new CultureInfo(item.Key)).ToList();
        }

        public List<System.Globalization.CultureInfo> GetPublishedPageLanguages()
        {
            var items = GetAll().Where(cp => cp.IsPublished).GroupBy(cp => cp.LanguageIsoCode);
            return items.Select(item => new CultureInfo(item.Key)).ToList();
        }
    }
}