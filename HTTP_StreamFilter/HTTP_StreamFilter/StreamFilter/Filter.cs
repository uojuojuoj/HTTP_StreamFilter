using HTTP_StreamFilter.JSON;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace HTTP_StreamFilter.StreamFilter
{
    class Filter
    {
        public ConcurrentQueue<Model> filtered = new ConcurrentQueue<Model>();
        public ConcurrentDictionary<int, int> counters = new ConcurrentDictionary<int, int>();
        public void RunFromFile(string fileFullPath)
        {
            using (StreamReader sr = new StreamReader(File.Open(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                string target;
                while (true)
                {
                    if ((target = sr.ReadLine()) == null)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    FilterOutUniqueIp(target);
                }
            }
        }

        private void FilterOutUniqueIp(string targetStr)
        {
            Model target = new Serializer().SerializeToObj(targetStr);

            // only status_code in range [500-599]
            if (!(target.status_code >= 500 && target.status_code <= 599))
                return;
            // only unique ips for same day
            if (filtered.Where(w => w.ip == target.ip && w.time.ToString("yyyyMMdd") == target.time.ToString("yyyyMMdd")).Any())
                return;
            // filter out weekends
            if (target.time.DayOfWeek == DayOfWeek.Saturday || target.time.DayOfWeek == DayOfWeek.Sunday)
                return;

            filtered.Enqueue(target);
            CountByHour(target);
        }

        private void CountByHour(Model target)
        {
            if (counters.Count != 0)
                foreach (KeyValuePair<int, int> kvp in counters)
                    if (kvp.Key == target.time.Hour)
                    {
                        counters[kvp.Key] += 1;
                        return;
                    }

            counters.AddOrUpdate(target.time.Hour, 1, (k, v) => v += 1);
        }
    }
}
