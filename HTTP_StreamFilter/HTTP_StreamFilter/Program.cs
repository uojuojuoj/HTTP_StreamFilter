using HTTP_StreamFilter.StreamFilter;
using HTTP_StreamFilter.StreamGenerator;
using System.IO;
using System.Linq;
using System.Threading;

namespace HTTP_StreamFilter
{
    class Program
    {
        static string path = @"/logs", filename = @"/logs.txt", filename2 = @"/logs2.txt", filenameResult = @"/result.txt";
        static string outDir = Directory.GetCurrentDirectory().ToString() + path;
        static string logPath = outDir + filename, logPath2 = outDir + filename2, outResult = outDir + filenameResult;
        static void Main(string[] args)
        {
            // Cleaning out dir
            DirectoryInfo outDirInfo = new DirectoryInfo(outDir);
            foreach (FileInfo f in outDirInfo.GetFiles())
                f.Delete();

            Generator generator = new Generator(outDir);
            Filter filter = new Filter();

            Thread thr1 = new Thread(() => generator.GenerateScriptNo1(logPath));
            Thread thr2 = new Thread(() => generator.GenerateScriptNo2(logPath2));
            //Thread thr1 = new Thread(() => generator.ExampleNo1(logPath));
            //Thread thr2 = new Thread(() => generator.ExampleNo2(logPath2));
            Thread thr3 = new Thread(() => filter.RunFromFile(logPath));
            Thread thr4 = new Thread(() => filter.RunFromFile(logPath2));

            thr1.Start();
            thr2.Start();
            Thread.Sleep(1000);
            thr3.Start();
            thr4.Start();
            Thread.Sleep(500);

            while (true)
            {
                Thread.Sleep(5000);
                File.WriteAllLines(outResult, filter.counters.Select(s => $"{s.Key.ToString()} - {s.Value.ToString()} unique user(s)").ToArray());
            }
        }
    }
}
