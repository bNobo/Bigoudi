using Bigoudi;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ExtensionsTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ExtensionsTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldExecuteDelegateForEachElement()
        {
            Mock<Action<int>> mockAction = new Mock<Action<int>>();

            int[] ints = { 1, 2, 3 };

            ints.ForEach(mockAction.Object);

            mockAction.Verify(action => action.Invoke(1));
            mockAction.Verify(action => action.Invoke(2));
            mockAction.Verify(action => action.Invoke(3));
        }

        [Fact]
        public void InnerLoopTest()
        {
            Mock<Action<string>> mockAction = new Mock<Action<string>>();

            int[] ints = { 1, 2, 3 };
            string[] strings = { "a", "b", "c" };

            ints.ForEach(i => strings.ForEach(mockAction.Object));

            mockAction.Verify(action => action.Invoke("a"), Times.Exactly(3));
            mockAction.Verify(action => action.Invoke("b"), Times.Exactly(3));
            mockAction.Verify(action => action.Invoke("c"), Times.Exactly(3));
        }

        [Fact]
        public void UsageExample()
        {
            int[] ints = { 1, 2, 3 };

            ints.ForEach(i =>
            {
                string[] strings = { "a", "b", "c" };
                strings.ForEach(s =>
                {
                    testOutputHelper.WriteLine($"i = {i}");
                    testOutputHelper.WriteLine($"s = {s}");
                });
            });
        }
    }
}
