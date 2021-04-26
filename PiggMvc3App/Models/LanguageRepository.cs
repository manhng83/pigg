using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Microsoft.Practices.Unity;
using Pigg.Contracts.Repositories;
using Pigg.Model;

namespace PiggMvc3App.Models
{
    public class LanguageRepository : ILanguageRepository
    {
        [Dependency]
        public ICustomPageRepository CustomPageRepository { get; set; }

        public IEnumerable<Pigg.Model.CustomPage> GetRootElements(string _languageName)
        {
            return CustomPageRepository.GetRootElements(_languageName);            
        }

        public List<System.Globalization.CultureInfo> GetLanguages()
        {
            var items = CustomPageRepository.GetAll().GroupBy(cp => cp.LanguageIsoCode);
            List<CultureInfo> langs = new List<CultureInfo>();
            foreach (var item in items)
                langs.Add(new CultureInfo(item.Key));
            return langs;
        }
    }
}