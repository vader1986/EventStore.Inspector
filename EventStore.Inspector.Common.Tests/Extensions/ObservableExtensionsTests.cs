using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using EventStore.Inspector.Common.Extensions;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Extensions
{
    [TestFixture]
    public class ObservableExtensionsTests
    {
        [Test]
        public async Task SlidingWindow_for_fixed_size_of_subsequent_elements()
        {
            var source = Observable.Range(0, 10);
            var slidingWindow = source.SlidingWindow(3);
            var windows = new List<IList<int>>();

            var lastWindow = await slidingWindow.Select(window =>
            {
                Assert.That(window.Count, Is.EqualTo(3));
                windows.Add(window);
                return window;
            }).ToTask();

            Assert.That(lastWindow[0], Is.EqualTo(7));
            Assert.That(lastWindow[1], Is.EqualTo(8));
            Assert.That(lastWindow[2], Is.EqualTo(9));

            Assert.That(windows.Count, Is.EqualTo(8));
        }

        [Test]
        public async Task SlidingWindow_for_elements_within_timeframe()
        {
            var start = new DateTime(2020, 1, 1);
            var timeFrame = TimeSpan.FromSeconds(2);
            var timeStamps = new[]
            {
                start,
                start.AddSeconds(1),
                start.AddSeconds(2),
                start.AddSeconds(3)
            };

            var source = Observable.Range(0, timeStamps.Length);
            var slidingWindow = source.SlidingWindow(i => timeStamps[i], timeFrame);
            var windows = new List<IList<int>>();

            var lastWindow = await slidingWindow.Select(window =>
            {
                Assert.That(window.Any());
                var windowStart = timeStamps[window[0]];

                Assert.That(window.All(x => timeStamps[x] - windowStart <= timeFrame));
                windows.Add(window);

                return window;
            }).ToTask();

            var expectedLastWindow = new List<int> {timeStamps.Length - 1};

            Assert.That(windows.Count, Is.EqualTo(4));
            Assert.That(lastWindow, Is.EquivalentTo(expectedLastWindow));
        }
    }
}
