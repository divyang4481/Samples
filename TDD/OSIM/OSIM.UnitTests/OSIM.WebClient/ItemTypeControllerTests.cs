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

        public class and_trying_to_create_a_new_valid_item_type : when_working_with_the_item_type_controller
        {
            private ItemType _newItemType;
            private ItemTypeController _target;
            private RedirectToRouteResult _result;
            private string _expectedRouteName;

            protected override void Establish_context()
            {
                base.Establish_context();
                _expectedRouteName = "index";
                _newItemType = new ItemType {Id = 99, Name = "New Item"};
                _target = new ItemTypeController(_sqlUnitOfWork.Object);
            }

            protected override void Because_of()
            {
                _result = _target.Create(_newItemType) as RedirectToRouteResult;
            }

            [Fact]
            public void then_a_valid_item_type_should_be_created_and_should_redirect_to_the_correct_view()
            {
                _result.ShouldNotBeNull();
                _result.RouteValues.Values.ShouldContain(_expectedRouteName);
            }
        }
    
        public class and_trying_to_create_a_new_invalid_item_type : when_working_with_the_item_type_controller
        {
            private ItemType _newItemType;
            private ItemTypeController _target;
            private ViewResult _result;
            private string _expectedRouteName;

            protected override void Establish_context()
            {
                base.Establish_context();
                _expectedRouteName = "create";
                _newItemType = new ItemType {Id = 99, Name = "New item"};
                _target = new ItemTypeController(_sqlUnitOfWork.Object);
                _target.ModelState.AddModelError("key", "Model is invalid");
            }

            protected override void Because_of()
            {
                _result = _target.Create(_newItemType) as ViewResult;
            }

            [Fact]
            public void then_a_new_item_type_should_not_be_created()
            {
                _result.ShouldNotBeNull();
                _result.ViewName.ShouldEqual(_expectedRouteName);
            }
        }

        public class and_trying_to_edit_an_existing_item : when_working_with_the_item_type_controller
        {
            private string _expecteRouteName;
            private ItemTypeController _target;
            private ViewResult _result;

            protected override void Establish_context()
            {
                base.Establish_context();
                _expecteRouteName = "edit";
                _target = new ItemTypeController(_sqlUnitOfWork.Object);
            }

            protected override void Because_of()
            {
                _result = _target.Edit(_itemTypeOne.Id) as ViewResult;
            }

            [Fact]
            public void then_a_valid_edit_view_should_be_returned()
            {
                _result.ViewName.ShouldEqual(_expecteRouteName);
            }
        }
    }
}
