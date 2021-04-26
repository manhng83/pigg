using System.ComponentModel.DataAnnotations;

namespace Pigg.Model
{
    [MetadataType(typeof(WebPartPlacementMetadata))]
    public class WebPartPlacement
    {
        public int WebPartPlacementId { get; set; }

        public string WebPartZone { get; set; }
    }

    internal class WebPartPlacementMetadata
    {
        [ScaffoldColumn(false)]
        [Key]
        public object WebPartPlacementId { get; set; }

        [StringLength(100)]
        [Required]
        public object WebPartZone { get; set; }
    }
}