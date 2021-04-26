using System.Collections.Generic;
using System.Globalization;
using Pigg.CQRS.ReadModel;
using Pigg.Contracts.Repositories;
using Pigg.Model;

namespace PiggMvc3App.Models
{
    public class CustomPageReader : ICustomPageReader
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

        public List<CultureInfo> GetPageLanguages()
        {
            return _customPageRepository.GetPageLanguages();
        }

        public List<CultureInfo> GetPublishedPageLanguages()
        {
            return _customPageRepository.GetPublishedPageLanguages();
        }

        public CustomPage GetById(int id)
        {
            return _customPageRepository.GetById(id);
        }

        public CustomPage GetById(System.Guid entityId)
        {
            return _customPageRepository.Get(cp => cp.EntityId == entityId);
        }
    }
}