using Pigg.Contracts.Repositories;
using Pigg.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pigg.Data.Repositories
{
    public class CustomPageRepository : RepositoryBase<CustomPage>, ICustomPageRepository
    {
        public CustomPageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IEnumerable<CustomPage> GetRootElements(string languageName)
        {
            var items = DataContext.CustomPages.AsNoTracking().Where(p => p.IsPublished && p.ParentCustomPageId == null && p.CultureCode == languageName);
            return items.OrderBy(p => p.OrderInList).AsEnumerable() ?? new List<CustomPage>();
        }

        public CustomPage GetFrontPage(string languageName)
        {
            try
            {
                CustomPage fp = (DataContext.CustomPages.FirstOrDefault(p => p.IsPublished && p.IsFrontPage && p.CultureCode == languageName) ??
                                 DataContext.CustomPages.FirstOrDefault(p => p.IsPublished && p.CultureCode == languageName)) ??
                                DataContext.CustomPages.FirstOrDefault(p => p.CultureCode == languageName);
                return fp == null ? new CustomPage
                {
                    Title = "Welcome",
                    LongTitle = "Welcome in this web site",
                    PageContent = "<p>Welcome in my new web site. I hope that you agree with me that this is an awesome web site.</p><p>&nbsp;</p><p>Admin user</p><p>username: admin<br />password: password</p>",
                    CultureCode = "en-GB",
                    IsPublished = true,
                    IsFrontPage = true,
                    ShowInList = true,
                    OrderInList = 0
                } : fp;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("There are not visible pages for the selected language");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public List<CultureInfo> GetPageLanguages()
        {
            var items = GetAll().GroupBy(cp => cp.CultureCode);
            return items.Select(item => new CultureInfo(item.Key)).ToList();
        }

        public List<CultureInfo> GetPublishedPageLanguages()
        {
            var items = GetAll().Where(cp => cp.IsPublished).GroupBy(cp => cp.CultureCode);
            return items.Select(item => new CultureInfo(item.Key)).ToList();
        }
    }
}