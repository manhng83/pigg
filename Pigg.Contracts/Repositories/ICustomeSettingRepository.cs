using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pigg.Model;
using System.Globalization;

namespace Pigg.Contracts.Repositories
{
    public interface ICustomSettingRepository : IRepository<CustomSetting>, IUnitOfWork
    {
        CustomSetting GetSettings(string Key);
    }
}
