using System;

namespace Pigg.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        PiggDbContext Get();
    }
}