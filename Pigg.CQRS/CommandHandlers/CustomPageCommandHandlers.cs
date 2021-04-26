using Pigg.Contracts.Repositories;
using Pigg.CQRS.Commands;
using Pigg.CQRS.Domain;
using System;

namespace Pigg.CQRS.CommandHandlers
{
    public class CustomPageCommandHandlers
    {
        private readonly ICustomPageRepository _repository;
        private readonly IRepository<CustomPage> _eventRepository;

        public CustomPageCommandHandlers(ICustomPageRepository repository, IRepository<CustomPage> eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
        }

        public void Handle(CreateCustomPage message)
        {
            var item = new CustomPage(message.PageId, message.Id, message.ParentId, message.LanguageIsoCode,
                                      message.Title, message.LongTitle, message.Description, message.PageContent,
                                      message.Keywords, message.IsPublished, message.IsFrontPage, message.ShowInList,
                                      message.OrderInList, message.IsExternal, message.ExternalUrl);

            _eventRepository.Save(item, -1);

            // TODO
            //var dto = Mapper.Map<CreateCustomPage, Model.CustomPage>(message);

            var dto = new Model.CustomPage
            {
                EntityId = item.Id,
                Description = message.Description,
                IsFrontPage = message.IsFrontPage,
                IsPublished = message.IsPublished,
                Keywords = message.Keywords,
                CultureCode = message.LanguageIsoCode,
                LongTitle = message.LongTitle,
                OrderInList = message.OrderInList,
                PageContent = message.PageContent,
                ParentCustomPageId = message.ParentId,
                ShowInList = message.ShowInList,
                Title = message.Title
            };

            _repository.Add(dto);
            _repository.Save();
        }

        public void Handle(UpdateCustomPage message)
        {
            throw new NotImplementedException();
            var item = new CustomPage(message.PageId, message.Id, message.ParentId, message.LanguageIsoCode,
                                      message.Title, message.LongTitle, message.Description, message.PageContent,
                                      message.Keywords, message.IsPublished, message.IsFrontPage, message.ShowInList,
                                      message.OrderInList, message.IsExternal, message.ExternalUrl);

            //var dto = Mapper.Map<UpdateCustomPage, Model.CustomPage>(message);

            //_repository.Update(dto);
            //_repository.Save();
            //_repository.Update(item, -1);
        }

        public void Handle(DeleteCustomPage message)
        {
            var itemToDelete = _repository.Get(i => i.EntityId == message.Id);
            _repository.Delete(itemToDelete);
            _repository.Save();
            //_repository.Delete(message.EntityId, -1);
        }
    }
}