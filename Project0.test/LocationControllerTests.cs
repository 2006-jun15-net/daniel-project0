using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.test
{
    public class LocationControllerTests
    {
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
