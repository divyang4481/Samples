using System;
using NBehave.Spec.Xunit;
using Ninject;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Xunit;

namespace OSIM.IntegrationTests.OSIM.Core.Persistence
{
    public class when_using_sql_unit_of_work : Specification
    {
        protected SqlUnitOfWork Target;
        protected StandardKernel Kernel;

        protected override void Establish_context()
        {
            base.Establish_context();
            Kernel = new StandardKernel(new IntegrationTestModule());
            Target = Kernel.Get<SqlUnitOfWork>();
        }

        public class and_attempting_to_add_and_commit_changes_and_read_item_type_from_database : when_using_sql_unit_of_work
        {
            private ItemType _expected;
            private ItemType _result;

            protected override void Establish_context()
            {
                base.Establish_context();

                _expected = new ItemType { Name = Guid.NewGuid().ToString() };
            }

            protected override void Because_of()
            {
                var itemType = Target.ItemTypes.Add(_expected);
                Target.Commit();
                _result = Target.ItemTypes.FindById(itemType.Id);
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
