using Pigg.Contracts.Repositories;
using Pigg.Model;

namespace Pigg.Data.Repositories
{
    public class ContentListRepository : RepositoryBase<ContentList>, IContentListRepository
    {
        public ContentListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}