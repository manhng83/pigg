using Pigg.Contracts;
using Pigg.Contracts.Repositories;
using Pigg.Data.Repositories;

namespace Pigg.Data
{
    public class PiggUow : IPiggUow
    {
        private IDatabaseFactory _dbFactory;
        private PiggDbContext _db;
        private IContentListItemRepository _contentListItemRepository;
        private IContentListRepository _contentListRepository;
        private ICustomSettingRepository _customSettingRepository;
        private ICustomPageRepository _customPageRepository;

        public PiggUow(IDatabaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _db = _dbFactory.Get();
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public IContentListItemRepository ContentListItemRepository
        {
            get
            {
                if (_contentListItemRepository == null)
                    _contentListItemRepository = new ContentListItemRepository(_dbFactory);
                return _contentListItemRepository;
            }
        }

        public IContentListRepository ContentListRepository
        {
            get
            {
                if (_contentListRepository == null)
                    _contentListRepository = new ContentListRepository(_dbFactory);
                return _contentListRepository;
            }
        }

        public ICustomPageRepository CustomPageRepository
        {
            get
            {
                if (_customPageRepository == null)
                    _customPageRepository = new CustomPageRepository(_dbFactory);
                return _customPageRepository;
            }
        }

        public ICustomSettingRepository CustomSettingRepository
        {
            get
            {
                if (_customSettingRepository == null)
                    _customSettingRepository = new CustomSettingRepository(_dbFactory);
                return _customSettingRepository;
            }
        }
    }
}