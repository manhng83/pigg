using Pigg.CQRS.ReadModel;
using Pigg.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace PiggMvc.Controllers.api
{
    public class CustomPagesController : ApiController
    {
        private IReadModelFacade _readModelFacade;

        public CustomPagesController(IReadModelFacade readModelFacade)
        {
            _readModelFacade = readModelFacade;
        }

        public IEnumerable<CustomPage> GetRootPages(string countryCode)
        {
            return _readModelFacade.GetRootElements(countryCode);
        }
    }
}