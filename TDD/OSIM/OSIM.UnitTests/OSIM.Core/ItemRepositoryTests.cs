using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Xunit;
using NBehave.Spec.Xunit;

namespace OSIM.UnitTests.OSIM.Core
{
    public class when_working_with_the_item_type_repository : Specification
    {}

    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {
        private int result;

        private ItemTypeRepository itemTypeRepository;
        private ItemType testItemType;
        private int itemTypeId;

        protected override void Because_of()
        {
            result = itemTypeRepository.Save(testItemType);
        }

        [Fact]
        public void then_a_valid_item_type_should_be_returned()
        {
            result.ShouldEqual(itemTypeId);
        }
    }
}
