using System;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConsoleOutputStream : IOutputStream
    {
        public void Append(string text)
        {
            Console.WriteLine(text);
        }
    }
}
