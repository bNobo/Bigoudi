﻿using Bigoudi;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTests;

public class LoopTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public LoopTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ForEachTest()
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
    public void ForTestDefault()
    {
        Mock<Action<int>> mockAction = new Mock<Action<int>>();

        Loop.For(mockAction.Object, 3);

        mockAction.Verify(action => action.Invoke(0));
        mockAction.Verify(action => action.Invoke(1));
        mockAction.Verify(action => action.Invoke(2));
        mockAction.Verify(action => action.Invoke(It.IsAny<int>()), Times.Exactly(3));
    }

    [Fact]
    public void ForTest()
    {
        Mock<Action<int>> mockAction = new Mock<Action<int>>();

        Loop.For(mockAction.Object, 6, 1, 2);

        mockAction.Verify(action => action.Invoke(1));
        mockAction.Verify(action => action.Invoke(3));
        mockAction.Verify(action => action.Invoke(5));
        mockAction.Verify(action => action.Invoke(It.IsAny<int>()), Times.Exactly(3));
    }

    [Fact]
    public void IterateTestDefault()
    {
        Mock<Action<int>> mockAction = new Mock<Action<int>>();

        Loop.Iterate(mockAction.Object, 3);

        mockAction.Verify(action => action.Invoke(0));
        mockAction.Verify(action => action.Invoke(1));
        mockAction.Verify(action => action.Invoke(2));
        mockAction.Verify(action => action.Invoke(It.IsAny<int>()), Times.Exactly(3));
    }

    [Fact]
    public void IterateTest()
    {
        Mock<Action<int>> mockAction = new Mock<Action<int>>();

        Loop.Iterate(mockAction.Object, 3, 1, 2);

        mockAction.Verify(action => action.Invoke(1));
        mockAction.Verify(action => action.Invoke(3));
        mockAction.Verify(action => action.Invoke(5));
        mockAction.Verify(action => action.Invoke(It.IsAny<int>()), Times.Exactly(3));
    }

    [Fact]
    public void WhileTest()
    {
        Mock<Action> mockAction = new Mock<Action>();
        int i = 0;

        Loop.While(mockAction.Object, () => i++ < 3);

        mockAction.Verify(action => action.Invoke(), Times.Exactly(3));
    }

    [Fact]
    public void DoWhileTest()
    {
        Mock<Action> mockAction = new Mock<Action>();
        int i = 0;

        Loop.DoWhile(mockAction.Object, () => i++ < 3);

        mockAction.Verify(action => action.Invoke(), Times.Exactly(4));
    }

    [Fact]
    public async Task InfiniteTest()
    {
        Mock<Action> mockAction = new Mock<Action>();
        CancellationTokenSource cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(1));

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            Task.Run(() => Loop.Infinite(cts.Token, mockAction.Object), cts.Token));

        mockAction.Verify(action => action.Invoke(), Times.AtLeast(100));
    }
} 
