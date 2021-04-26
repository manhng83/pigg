using Pigg.CQRS.Events;
using System;

namespace Pigg.CQRS.Domain
{
    public class CustomPage : AggregateRoot
    {
        private Guid _id;

        public override Guid Id
        {
            get { return _id; }
        }

        public void Remove(Guid entityId)
        {
            ApplyChange(new CustomPageDeleted(entityId));
        }

        public void Update(int pageId, Guid entityId, int? parentId, string languageIsoCode, string title, string longTitle, string description, string pageContent, string keywords, bool isPublished, bool isFrontPage, bool showInList, decimal? orderInList, bool isExternal, string externalUrl)
        {
            ApplyChange(new CustomPageUpdated(pageId, entityId, parentId, languageIsoCode, title, longTitle, description,
                                              pageContent, keywords, isPublished, isFrontPage, showInList, orderInList,
                                              isExternal, externalUrl));
        }

        private void Apply(CustomPageCreated e)
        {
            _id = e.EntityId;
        }

        public CustomPage()
        {
            // used to create in repository ... many ways to avoid this, eg making private constructor
        }

        public CustomPage(int pageId, Guid entityId, int? parentId, string languageIsoCode, string title, string longTitle, string description, string pageContent, string keywords, bool isPublished, bool isFrontPage, bool showInList, decimal? orderInList, bool isExternal, string externalUrl)
        {
            ApplyChange(new CustomPageCreated(pageId, entityId, parentId, languageIsoCode, title, longTitle, description,
                                              pageContent, keywords, isPublished, isFrontPage, showInList, orderInList,
                                              isExternal, externalUrl));
        }
    }
}