using HTTP_StreamFilter.Includes;
using HTTP_StreamFilter.JSON;
using System;
using System.IO;
using System.Threading;

namespace HTTP_StreamFilter.StreamGenerator
{
    class Generator
    {
        private string outDir = "";
        private int iterCount = 0;

        public Generator(string outDir, int iterCount = 100000000)
        {
            this.outDir = outDir;
            this.iterCount = iterCount;
        }

        public void GenerateScriptNo1(string fullPath)
        {
            Serializer jsonSerializer = new Serializer();
            using (StreamWriter sw = new StreamWriter(File.Open(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.AutoFlush = true;
                for (int i = 1; i < iterCount; i++)
                {
                    int part1 = i * 191 % 256;
                    int part2 = i * 219 % 250;
                    string ipStr = $"10.0.{part1}.{part2}";
                    DateTime dateStr = StringHelper.UnixTimeStampToDateTime(1557824751 + i);
                    int code = (i * 55 / 6 % 4 + 2) * 100 + (i * 19 % 4);

                    Thread.Sleep(10);

                    sw.WriteLine(jsonSerializer.SerializeToString(new Model { time = dateStr, ip = ipStr, status_code = code }));
                }
            }
        }
        public void GenerateScriptNo2(string fullPath)
        {
            Serializer jsonSerializer = new Serializer();
            using (StreamWriter sw = new StreamWriter(File.Open(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.AutoFlush = true;
                for (int i = 1; i < iterCount; i++)
                {
                    int part1 = i * 19 % 256;
                    int part2 = i * 37 % 250;
                    string ipStr = $"10.0.{part1}.{part2}";
                    DateTime dateStr = StringHelper.UnixTimeStampToDateTime(1420063200 + i * 2);
                    int code = (i * 8 / 6 % 4 + 2) * 100 + (i * 13 % 4);

                    Thread.Sleep(10);

                    sw.WriteLine(jsonSerializer.SerializeToString(new Model { time = dateStr, ip = ipStr, status_code = code }));
                }
            }
        }
        public void ExampleNo1(string fullPath)
        {
            string example = "{\"time\":\"2019 - 05 - 06 17:24:53\",\"ip\":\"10.0.186.98\",\"status_code\":202}\r\n" +
                             "{\"time\":\"2019-05-06 17:24:54\",\"ip\":\"10.0.121.67\",\"status_code\":301}\r\n" +
                             "{\"time\":\"2019-05-06 17:25:54\",\"ip\":\"10.0.121.67\",\"status_code\":500}\r\n" +
                             "{\"time\":\"2019-05-06 17:26:55\",\"ip\":\"10.0.121.67\",\"status_code\":500}\r\n" +
                             "{\"time\":\"2019-05-07 16:59:55\",\"ip\":\"10.0.121.67\",\"status_code\":502}\r\n" +
                             "{\"time\":\"2019-05-07 17:00:56\",\"ip\":\"10.0.247.5\",\"status_code\":503}\r\n" +
                             "{\"time\":\"2019-05-11 10:24:57\",\"ip\":\"10.0.182.224\",\"status_code\":502}\r\n" +
                             "{\"time\":\"2019-05-11 10:24:58\",\"ip\":\"10.0.117.193\",\"status_code\":401}\r\n" +
                             "{\"time\":\"2019-05-12 10:24:59\",\"ip\":\"10.0.52.162\",\"status_code\":500}\r\n";
            File.WriteAllText(fullPath, example);
        }
        public void ExampleNo2(string fullPath)
        {
            string example = "{\"time\":\"2020-01-01 17:24:53\",\"ip\":\"10.0.186.98\",\"status_code\":502}\r\n" +
                             "{\"time\":\"2020-01-01 17:24:54\",\"ip\":\"10.0.121.67\",\"status_code\":301}\r\n" +
                             "{\"time\":\"2020-01-01 17:25:54\",\"ip\":\"10.0.121.67\",\"status_code\":500}\r\n";
            File.WriteAllText(fullPath, example);
        }
    }
}
