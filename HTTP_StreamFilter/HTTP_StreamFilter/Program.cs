using HTTP_StreamFilter.JSON;
using HTTP_StreamFilter.StreamFilter;
using HTTP_StreamFilter.StreamGenerator;
using System;
using System.Threading;

namespace HTTP_StreamFilter
{
    class Program
    {
        static string arguments = "";
        static void Main(string[] args)
        {
            while (string.IsNullOrEmpty(arguments))
            {
                Console.WriteLine("Enter filter arguments");
                arguments = Console.ReadLine();
            }

            Generator generator = new Generator();
            Filter filter = new Filter();

            Thread thr1 = new Thread(generator.GenerateScriptNo1);
            Thread thr2 = new Thread(generator.GenerateScriptNo2);

            thr1.Start();
            thr2.Start();
            Thread.Sleep(1000);

            Model target = new Model();
            while (!generator.lines.IsEmpty && generator.lines.TryDequeue(out target))
                filter.FilterOut(arguments, target);

        }
    }
}
