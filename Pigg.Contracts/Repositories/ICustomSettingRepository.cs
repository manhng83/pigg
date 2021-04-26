using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pigg.Model;

namespace Pigg.Contracts.Repositories
{
    public interface ICustomSettingRepository
    {
        CustomSetting GetSettings(string Key);
    }
}
