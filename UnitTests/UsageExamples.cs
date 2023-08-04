using Bigoudi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UsageExamples
    {
        private readonly ITestOutputHelper testOutputHelper;

        public UsageExamples(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ForEachUsageExample()
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


        [Fact]
        public void ForEachExtensionUsageExample()
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

        [Fact]
        public void ForUsageExample()
        {
            Loop.For(i => testOutputHelper.WriteLine($"i = {i}"), 6, 1, 2);
        }

        [Fact]
        public void IterateUsageExample()
        {
            Loop.Iterate(i => testOutputHelper.WriteLine($"i = {i}"), 3, 1, 2);
        }

        [Fact]
        public void WhileUsageExample()
        {
            int i = 0;
            Loop.While(() => testOutputHelper.WriteLine($"i = {i}"), () => i++ < 3);
        }

        [Fact]
        public void DoWhileUsageExample()
        {
            int i = 0;
            Loop.DoWhile(() => testOutputHelper.WriteLine($"i = {i}"), () => i++ < 3);
        }

        [Fact]
        public async Task InfiniteUsageExample()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(1));

            try
            {
                var execute = () => { testOutputHelper.WriteLine("execute..."); Thread.Sleep(100); };
                await Task.Run(() => Loop.Infinite(cts.Token, execute), cts.Token);
            }
            catch (OperationCanceledException)
            {
                testOutputHelper.WriteLine("loop ended");
            }
        }
    }
}
