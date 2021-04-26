namespace Pigg.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private PiggDbContext _dataContext;

        public PiggDbContext Get()
        {
            return _dataContext ?? (_dataContext = new PiggDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}