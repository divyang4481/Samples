using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using OSIM.WebClient.Controllers;
using Xunit;
using NBehave.Spec.Xunit;

namespace OSIM.UnitTests.OSIM.WebClient
{
    public class when_working_with_the_item_type_controller : Specification
    {
        protected Mock<IUnitOfWork> _sqlUnitOfWork;
        protected Mock<IRepository<ItemType>> _itemTypeRepository;

        protected ItemType _itemTypeOne;
        protected ItemType _itemTypeTwo;
        protected ItemType _itemTypeThree;

        protected override void Establish_context()
        {
            _sqlUnitOfWork = new Mock<IUnitOfWork>();
            _itemTypeRepository = new Mock<IRepository<ItemType>>();

            _itemTypeOne = new ItemType {Id = 1, Name = "Ssaki"};
            _itemTypeTwo = new ItemType {Id = 2, Name = "Śruby"};
            _itemTypeThree = new ItemType {Id = 3, Name = "Ewangeliści"};

            var itemTypeList = new List<ItemType> {_itemTypeOne, _itemTypeTwo, _itemTypeThree};

            _itemTypeRepository.Setup(r => r.FindAll()).Returns(itemTypeList);
            _sqlUnitOfWork.Setup(s => s.ItemTypes).Returns(_itemTypeRepository.Object);
        }

        public class and_trying_to_load_the_index_page : when_working_with_the_item_type_controller
        {
            private object _model;
            private int _expectedNumberOfItemsInModel;

            protected override void Establish_context()
            {
                base.Establish_context();
                _expectedNumberOfItemsInModel = _sqlUnitOfWork.Object.ItemTypes.FindAll().Count();
            }

            protected override void Because_of()
            {
                _model = ((ViewResult)new ItemTypeController(_sqlUnitOfWork.Object).Index()).ViewData.Model;
            }

            [Fact]
            public void then_a_valid_list_of_items_should_be_returned_in_the_model()
            {
                _expectedNumberOfItemsInModel.ShouldEqual(((List<ItemType>)_model).Count);
                _itemTypeOne.ShouldEqual(((List<ItemType>)_model)[0]);
            }
        }
    }

}
