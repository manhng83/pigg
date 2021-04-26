using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pigg.Model
{
    [MetadataType(typeof(ContentListItemMetadata))]
    public class ContentListItem
    {
        public int ContentListItemId { get; set; }

        public int ContentListId { get; set; }

        public string LanguageIsoCode { get; set; }

        public int? ParentContentListItemId { get; set; }

        public string Title { get; set; }

        public string LongTitle { get; set; }

        public string Description { get; set; }

        public string PageContent { get; set; }

        public string Keywords { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFrontPage { get; set; }

        public bool ShowInList { get; set; }

        public decimal? OrderInList { get; set; }

        public ContentList ContentList { get; set; }
    }

    internal class ContentListItemMetadata
    {
        [ScaffoldColumn(false)]
        public object ContentListItemId { get; set; }

        public object ContentListId { get; set; }

        [UIHint("LanguageList")]
        [DisplayName("Language")]
        [Required]
        [StringLength(15)]
        public object LanguageIsoCode { get; set; }

        [ScaffoldColumn(false)]
        public object ParentContentListItemId { get; set; }

        [Required]
        [StringLength(255)]
        public object Title { get; set; }

        [StringLength(255)]
        public object LongTitle { get; set; }

        public object Description { get; set; }

        [DataType(DataType.MultilineText)]
        public object PageContent { get; set; }

        public object Keywords { get; set; }

        [Required]
        public object IsPublished { get; set; }

        [Required]
        public object IsFrontPage { get; set; }

        [Required]
        public object ShowInList { get; set; }

        public object OrderInList { get; set; }

        public object ContentList { get; set; }
    }
}