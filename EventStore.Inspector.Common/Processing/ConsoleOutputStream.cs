using System;

namespace EventStore.Inspector.Common.Processing
{
    public class ConsoleOutputStream : IOutputStream
    {
        public void Append(string text)
        {
            Console.WriteLine(text);
        }
    }
}
