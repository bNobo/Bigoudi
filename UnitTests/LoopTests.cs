﻿using Bigoudi;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTests
{
    public class LoopTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public LoopTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldExecuteDelegateForEachElement()
        {
            Mock<Action<int>> mockAction = new Mock<Action<int>>();

            int[] ints = { 1, 2, 3 };

            Loop.ForEach(ints, mockAction.Object);

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

            Loop.ForEach(ints, i => Loop.ForEach(strings, mockAction.Object));

            mockAction.Verify(action => action.Invoke("a"), Times.Exactly(3));
            mockAction.Verify(action => action.Invoke("b"), Times.Exactly(3));
            mockAction.Verify(action => action.Invoke("c"), Times.Exactly(3));
        }

        [Fact]
        public void UsageExample()
        {
            int[] ints = { 1, 2, 3 };

            Loop.ForEach(ints, i =>
            {
                string[] strings = { "a", "b", "c" };
                Loop.ForEach(strings, s =>
                {
                    testOutputHelper.WriteLine($"i = {i}");
                    testOutputHelper.WriteLine($"s = {s}");
                });
            });
        }
    }
}