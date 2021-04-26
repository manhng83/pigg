using System.ComponentModel.DataAnnotations;

namespace Pigg.Model
{
    [MetadataType(typeof(CustomSettingMetadata))]
    public partial class CustomSetting
    {
        public int CustomSettingId { get; set; }

        public string Key { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public bool Reserved { get; set; }
    }

    internal class CustomSettingMetadata
    {
        [Key]
        public object CustomSettingId { get; set; }

        [Required]
        [StringLength(150)]
        public object Key { get; set; }

        public object Title { get; set; }

        [Required]
        public object Value { get; set; }

        public object Description { get; set; }

        public object Reserved { get; set; }
    }
}