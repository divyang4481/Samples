using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIM.Core.Entities;
using NBehave.Spec.Xunit;
using OSIM.Core.Persistence;
using Xunit;
using Ninject;

namespace OSIM.IntegrationTests.OSIM.Core.Persistence
{
    public class ItemTypeRepositoryTests
    {
        public class when_using_the_item_type_repository : Specification
        {
            protected ItemTypeRepository _target;
            protected StandardKernel _kernel;

            protected override void Establish_context()
            {
                base.Establish_context();
                _kernel = new StandardKernel(new IntegrationTestModule());
                _target = _kernel.Get<ItemTypeRepository>();
            }
        }

        public class and_attempting_to_add_and_read_an_item_type_from_the_database : when_using_the_item_type_repository
        {
            private ItemType _expected;
            private ItemType _result;
            
            protected override void Establish_context()
            {
                base.Establish_context();
                
                _expected = new ItemType {Name = new Guid().ToString()};
            }

            protected override void Because_of()
            {
                var itemType = _target.Add(_expected);
                _result = _target.FindById(itemType.Id);
            }

            [Fact]
            public void then_the_item_saved_to_the_database_should_equal_the_item_type()
            {
                _result.Id.ShouldEqual(_expected.Id);
                _result.Name.ShouldEqual(_expected.Name);
            }
        }
    }
}
