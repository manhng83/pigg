using Pigg.CQRS.ReadModel;
using Pigg.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PiggMvc.Models
{
    public class ReadModelFacade : IReadModelFacade
    {
        private PiggDbContext _db;
        private IDatabaseFactory _databaseFactory;

        public ReadModelFacade(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _db = _databaseFactory.Get();
        }

        public IEnumerable<Pigg.Model.CustomPage> GetAll()
        {
            return _db.CustomPages.AsNoTracking().ToList();
        }

        public IEnumerable<Pigg.Model.CustomPage> GetRootElements(string cultureCode)
        {
            return _db.CustomPages
                .AsNoTracking()
                .Where(c => c.ParentCustomPageId.HasValue == false && c.CultureCode == cultureCode && c.IsPublished)
                .OrderBy(c => c.OrderInList).ToList();
        }

        public Pigg.Model.CustomPage GetFrontPage(string cultureCode)
        {
            return _db.CustomPages
                .AsNoTracking()
                .FirstOrDefault(c => c.IsFrontPage == true && c.IsPublished && c.CultureCode == cultureCode);
        }

        private List<CultureInfo> _cultures;

        public List<System.Globalization.CultureInfo> GetCultures()
        {
            if (_cultures == null)
                _cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).ToList();
            return _cultures;
        }

        public List<System.Globalization.CultureInfo> GetExistentCultures()
        {
            throw new NotImplementedException();
        }

        public Pigg.Model.CustomPage GetPage(int customPageId)
        {
            return _db.CustomPages.AsNoTracking().FirstOrDefault(c => c.CustomPageId == customPageId);
        }

        public Pigg.Model.CustomPage GetPage(Guid entityId)
        {
            return _db.CustomPages.AsNoTracking().FirstOrDefault(c => c.EntityId == entityId);
        }
    }
}