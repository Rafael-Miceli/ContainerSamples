using System;
using TruthyExtension;
using Xunit;

namespace app_api_tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void When_True_Return_Bool_True()
        {
            var value = "True";

            Assert.Equal(true, value.ToTruthy());
        }

        [Fact]
        public void When_False_Return_Bool_False()
        {
            var value = "False";

            Assert.Equal(false, value.ToTruthy());
        }

        [Fact]
        public void When_Greater_Than_0_Return_Bool_True()
        {
            var value = "1";
            
            Assert.Equal(true, value.ToTruthy());
        }

        [Fact]
        public void When_0_Return_Bool_False()
        {
            var value = "0";

            Assert.Equal(false, value.ToTruthy());
        }

        [Fact]
        public void When_NonEmptyString_Return_Bool_True()
        {
            var value = "test";

            Assert.Equal(true, value.ToTruthy());
        }

        [Fact]
        public void When_EmptyString_Return_Bool_False()
        {
            var value = "";

            Assert.Equal(false, value.ToTruthy());
        }
    }
}
