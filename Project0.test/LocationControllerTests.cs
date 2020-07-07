using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project0.Library.models;
using Xunit;

namespace Project0.test
{
    public class LocationControllerTests
    {
        [Fact]
        public void DummyTest()
        {
            //LocationController.Options = new DbContextOptions<DBAccess.Model.Project01Context>()
            //    .UseInMemoryDatabase("TestDatabase")
            //    .Options;
            Assert.True(true);
        }


        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
