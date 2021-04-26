using Pigg.Contracts.Repositories;
using Pigg.CQRS.ReadModel;
using Pigg.Model;
using System.Collections.Generic;
using System.Globalization;

namespace PiggMvc.Models
{
    public class CustomPageReader : IReadModelFacade
    {
        private readonly ICustomPageRepository _customPageRepository;

        public CustomPageReader(ICustomPageRepository customPageRepository)
        {
            _customPageRepository = customPageRepository;
        }

        public IEnumerable<CustomPage> GetAll()
        {
            return _customPageRepository.GetAll();
        }

        public IEnumerable<CustomPage> GetRootElements(string languageName)
        {
            return _customPageRepository.GetRootElements(languageName);
        }

        public CustomPage GetFrontPage(string languageName)
        {
            return _customPageRepository.GetFrontPage(languageName);
        }

        public List<CultureInfo> GetCultures()
        {
            return _customPageRepository.GetPageLanguages();
        }

        public List<CultureInfo> GetExistentCultures()
        {
            return _customPageRepository.GetPublishedPageLanguages();
        }

        public CustomPage GetPage(int id)
        {
            return _customPageRepository.GetById(id);
        }

        public CustomPage GetPage(System.Guid entityId)
        {
            return _customPageRepository.Get(cp => cp.EntityId == entityId);
        }
    }
}