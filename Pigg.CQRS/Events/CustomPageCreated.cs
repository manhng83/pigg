using System;

namespace Pigg.CQRS.Events
{
    public class CustomPageCreated : DomainEvent
    {
        public int PageId { get; set; }

        public Guid EntityId { get; set; }

        public int? ParentId { get; set; }

        public string LanguageIsoCode { get; set; }

        public string Title { get; set; }

        public string LongTitle { get; set; }

        public string Description { get; set; }

        public string PageContent { get; set; }

        public string Keywords { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFrontPage { get; set; }

        public bool ShowInList { get; set; }

        public decimal? OrderInList { get; set; }

        public bool IsExternal { get; set; }

        public string ExternalUrl { get; set; }

        public CustomPageCreated(int pageId, Guid entityId, int? parentId, string languageIsoCode, string title, string longTitle, string description, string pageContent, string keywords, bool isPublished, bool isFrontPage, bool showInList, decimal? orderInList, bool isExternal, string externalUrl)
        {
            PageId = pageId;
            EntityId = entityId;
            ParentId = parentId;
            LanguageIsoCode = languageIsoCode;
            Title = title;
            LongTitle = longTitle;
            Description = description;
            PageContent = pageContent;
            Keywords = keywords;
            IsPublished = isPublished;
            IsFrontPage = isFrontPage;
            ShowInList = showInList;
            OrderInList = orderInList;
            IsExternal = isExternal;
            ExternalUrl = externalUrl;
        }
    }
}