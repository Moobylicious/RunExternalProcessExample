using System;

namespace Some64BitProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            //this just echoes back what was passed in, but this is a 64-bit process.
            Console.WriteLine("I'm a 64-bit process!");
            Console.WriteLine($"I received {string.Join(",", args)}");
        }
    }
}
