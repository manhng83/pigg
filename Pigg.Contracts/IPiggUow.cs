using Pigg.Contracts.Repositories;

namespace Pigg.Contracts
{
    public interface IPiggUow
    {
        int Save();

        IContentListItemRepository ContentListItemRepository { get; }
        IContentListRepository ContentListRepository { get; }
        ICustomPageRepository CustomPageRepository { get; }
        ICustomSettingRepository CustomSettingRepository { get; }
    }
}