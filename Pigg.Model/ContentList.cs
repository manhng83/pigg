using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pigg.Model
{
    [MetadataType(typeof(ContentListMetadata))]
    public class ContentList
    {
        public int ContentListId { get; set; }

        public int? WebPartPlacementId { get; set; }

        public bool Visible { get; set; }

        public WebPartPlacement WebPartPlacement { get; set; }

        public ICollection<ContentListItem> ContentListItems { get; set; }
    }

    internal class ContentListMetadata
    {
        [ScaffoldColumn(false)]
        public object ContentListId { get; set; }

        [ScaffoldColumn(false)]
        public object WebPartPlacementId { get; set; }

        public object WebPartPlacement { get; set; }

        public object ContentListItems { get; set; }
    }
}