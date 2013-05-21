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
    {
        protected ItemTypeRepository _itemTypeRepository;
        protected Mock<IDbContext> _dbContext;
        protected Mock<IDbSet<ItemType>> _dbSet; 

        protected override void Establish_context()
        {
            base.Establish_context();

            _dbContext = new Mock<IDbContext>();
            _dbSet = new Mock<IDbSet<ItemType>>();
            _dbContext.Setup(c => c.Set<ItemType>()).Returns(_dbSet.Object);

            _itemTypeRepository = new ItemTypeRepository(_dbContext.Object);
        }
    }

    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {
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

            _dbSet.Setup(d => d.Add(_testItemType)).Returns(_testItemType);
        }

        [Fact]
        public void then_a_valid_item_type_should_be_returned()
        {
            _resultItemType.Id.ShouldEqual(_testItemType.Id);
            _resultItemType.Name.ShouldEqual(_testItemType.Name);
        }
    }

    public class and_saving_invalid_item_type : when_working_with_the_item_type_repository
    {
        private Exception _result;
        private ItemType _testItemType;

        protected override void Establish_context()
        {
            base.Establish_context();

            _dbSet.Setup(d => d.Add(null)).Throws<ArgumentNullException>();

            _testItemType = null;
        }

        protected override void Because_of()
        {
            try
            {
                _itemTypeRepository.Add(_testItemType);
            }
            catch (Exception exception)
            {
                _result = exception;
            }
        }

        [Fact]
        public void then_an_argument_null_exception_should_be_raised()
        {
            _result.ShouldBeInstanceOfType(typeof(ArgumentNullException));
        }
    }
}
