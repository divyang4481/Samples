using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public interface IItemTypeRepository
    {
        int Save(ItemType itemType);
    }

    public class ItemTypeRepository : IItemTypeRepository
    {
        public int Save(ItemType itemType)
        {
            throw new System.NotImplementedException();
        }
    }
}
