﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Xunit;
using NBehave.Spec.Xunit;
using Moq;

namespace OSIM.UnitTests.OSIM.Core.Persistence
{
    public class when_working_with_the_item_type_repository : Specification
    {
        protected ItemTypeRepository _target;
        protected Mock<IDbContext> _dbContext;
        protected Mock<IDbSet<ItemType>> _dbSet;
        
        protected override void Establish_context()
        {
            base.Establish_context();

            _dbContext = new Mock<IDbContext>();
            _dbSet = new Mock<IDbSet<ItemType>>();
            
            _dbContext.Setup(c => c.Set<ItemType>()).Returns(_dbSet.Object);
            
            _target = new ItemTypeRepository(_dbContext.Object);

        }

        public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
        {
            private ItemType _testItemType;
            private ItemType _result;

            protected override void Establish_context()
            {
                base.Establish_context();

                var name = new Guid().ToString();

                var randomNumberGenerator = new Random();
                int itemTypeId = randomNumberGenerator.Next(3000);

                _testItemType = new ItemType {Id = itemTypeId, Name = name};

                _dbSet.Setup(d => d.Add(_testItemType)).Returns(_testItemType);
            }

            protected override void Because_of()
            {
                _result = _target.Add(_testItemType);
            }

            [Fact]
            public void then_a_valid_item_type_should_be_returned()
            {
                _result.Id.ShouldEqual(_testItemType.Id);
                _result.Name.ShouldEqual(_testItemType.Name);
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
                    _target.Add(_testItemType);
                }
                catch (Exception exception)
                {
                    _result = exception;
                }
            }

            [Fact]
            public void then_an_argument_null_exception_should_be_raised()
            {
                _result.ShouldBeInstanceOfType(typeof (ArgumentNullException));
            }
        }

        //public  class and_getting_all_item_types : when_working_with_the_item_type_repository
        //{
        //    private List<ItemType> _itemTypesList;
        //    private IEnumerable<ItemType> _result;

        //    protected override void Establish_context()
        //    {
        //        base.Establish_context();

        //        _itemTypesList = new List<ItemType>
        //            {
        //                new ItemType {Id = 0, Name = Guid.NewGuid().ToString()},
        //                new ItemType {Id = 1, Name = Guid.NewGuid().ToString()}
        //            };

        //        _dbContext.Setup(c => c.Set<ItemType>()).Returns((IDbSet<ItemType>)_itemTypesList);                
                
        //    }

        //    protected override void Because_of()
        //    {
        //        _result = _target.FindAll();
        //    }

        //    [Fact]
        //    public void then_a_list_of_all_item_types_should_be_returned()
        //    {
        //        var resultList = _result.ToList();

        //        resultList[0].Id.ShouldEqual(_itemTypesList[0].Id);
        //        resultList[0].Name.ShouldEqual(_itemTypesList[1].Name);

        //        resultList[1].Id.ShouldEqual(_itemTypesList[1].Id);
        //        resultList[1].Name.ShouldEqual(_itemTypesList[1].Name);
        //    }
        //}
    }
}
