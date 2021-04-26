using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pigg.Model;
using System.Globalization;

namespace PiggMvc3App.Models
{
    public interface ILanguageRepository
    {
        IEnumerable<CustomPage> GetRootElements(string _languageName);
        List<CultureInfo> GetLanguages();
    }
}
