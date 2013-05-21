using System;
using System.Data.Entity;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Xunit;
using NBehave.Spec.Xunit;
using Moq;

namespace OSIM.UnitTests.OSIM.Core
{
    public class when_working_with_the_item_type_repository : Specification
    {}

    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {
        private ItemTypeRepository _itemTypeRepository;
        private ItemType _testItemType;
        private ItemType _resultItemType;

        protected override void Because_of()
        {
            _resultItemType = _itemTypeRepository.Add(_testItemType);
        }

        protected override void Establish_context()
        {
            base.Establish_context();

            var name = new Guid().ToString();

            var randomNumberGenerator = new Random();
            int itemTypeId = randomNumberGenerator.Next(3000);

            _testItemType = new ItemType {Id = itemTypeId, Name = name};

            var context = new Mock<IDbContext>();
            var dbSet = new Mock<IDbSet<ItemType>>();
            dbSet.Setup(d => d.Add(_testItemType)).Returns(_testItemType);

            context.Setup(c => c.Set<ItemType>()).Returns(dbSet.Object);

            _itemTypeRepository = new ItemTypeRepository(context.Object);
        }

        [Fact]
        public void then_a_valid_item_type_should_be_returned()
        {
            _resultItemType.Id.ShouldEqual(_testItemType.Id);
            _resultItemType.Name.ShouldEqual(_testItemType.Name);
        }
    }
}
