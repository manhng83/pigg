using Pigg.CQRS.Domain;
using System;

namespace Pigg.CQRS
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);

        T GetById(Guid id);
    }
}