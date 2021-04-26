using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pigg.Model
{
    public class CustomSetting
    {
        public virtual Guid ConfigId {get; set; }

        public virtual Guid LanguageId {get; set; }

        public virtual string Key {get; set; }

        public virtual string Title { get; set; }

        public virtual string Value { get; set; }

        public virtual string Description { get; set; }
        
        public virtual bool Reserved { get; set; }

        public virtual string FilePath { get; set; }
    }
}
