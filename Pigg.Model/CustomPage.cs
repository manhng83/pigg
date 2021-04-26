using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pigg.Model
{
    [MetadataType(typeof(CustomPageMetadata))]
    public partial class CustomPage
    {
        public virtual int CustomPageId { get; set; }

        public virtual int? ParentCustomPageId { get; set; }

        public virtual string CultureCode { get; set; }

        public virtual string Title { get; set; }

        public virtual string LongTitle { get; set; }

        public virtual string Description { get; set; }

        public virtual string PageContent { get; set; }

        public virtual string Keywords { get; set; }

        public virtual bool IsPublished { get; set; }

        public virtual bool IsFrontPage { get; set; }

        public virtual bool ShowInList { get; set; }

        public virtual decimal? OrderInList { get; set; }

        public Guid EntityId { get; set; }

        public virtual ICollection<CustomPage> CustomPage1 { get; set; }
        public virtual CustomPage CustomPage2 { get; set; }
    }

    public class CustomPageMetadata
    {
        [ScaffoldColumn(false)]
        [Key]
        public object CustomPageId { get; set; }

        [UIHint("LanguageList")]
        [DisplayName("Language")]
        [Required]
        [StringLength(15)]
        public string CultureCode { get; set; }

        [ScaffoldColumn(false)]
        public object ParentCustomPageId { get; set; }

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

        [ScaffoldColumn(false)]
        public ICollection<CustomPage> CustomPage1 { get; set; }

        [ScaffoldColumn(false)]
        public CustomPage CustomPage2 { get; set; }
    }
}