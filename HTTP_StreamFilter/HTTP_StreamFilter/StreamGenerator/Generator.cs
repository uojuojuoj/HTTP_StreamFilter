using HTTP_StreamFilter.Includes;
using HTTP_StreamFilter.JSON;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace HTTP_StreamFilter.StreamGenerator
{
    class Generator
    {
        private string outDir = "";
        private int iterCount = 0;

        private Serializer jsonSerializer = new Serializer();
        public ConcurrentQueue<Model> lines = new ConcurrentQueue<Model>();

        public Generator(string outDir = "", int iterCount = 100000000)
        {
            this.outDir = outDir;
            this.iterCount = iterCount;
        }

        public void GenerateScriptNo1()
        {
            for (int i = 1; i < iterCount; i++)
            {
                int part1 = i * 191 % 256;
                int part2 = i * 219 % 250;
                string ipStr = $"10.0.{part1}.{part2}";
                DateTime dateStr = StringHelper.UnixTimeStampToDateTime(1557824751 + i * 2);
                int code = (i * 55 / 6 % 4 + 2) * 100 + (i * 19 % 4);

                while (this.lines.Count() > 99999)
                    Thread.Sleep(10);

                this.lines.Enqueue(new Model { Ip = ipStr, DateTime = dateStr, StatusCode = code });
            }
        }
        public void GenerateScriptNo2()
        {
            for (int i = 1; i < iterCount; i++)
            {
                int part1 = i * 191 % 256;
                int part2 = i * 219 % 250;
                string ipStr = $"10.0.{part1}.{part2}";
                DateTime dateStr = StringHelper.UnixTimeStampToDateTime(1557824751 + i * 2);
                int code = (i * 55 / 6 % 4 + 2) * 100 + (i * 19 % 4);

                while (this.lines.Count() > 99999)
                    Thread.Sleep(10);

                this.lines.Enqueue(new Model { Ip = ipStr, DateTime = dateStr, StatusCode = code });
            }
        }
    }
}
