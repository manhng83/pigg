using Pigg.Contracts.Repositories;
using Pigg.Model;

namespace Pigg.Data.Repositories
{
    public class WebPartPlacementRepository : RepositoryBase<WebPartPlacement>, IWebPartPlacementRepository
    {
        public WebPartPlacementRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }
    }
}