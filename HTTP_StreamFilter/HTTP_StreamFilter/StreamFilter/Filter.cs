using HTTP_StreamFilter.JSON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HTTP_StreamFilter.StreamFilter
{
    class Filter
    {
        private DateTime? dateTimeFrom, dateTimeTo;
        private int[] range = { 0, 0 };
        private bool unique;

        private FilterArgs args = new FilterArgs
        {
            DateTo = @"-dto\s(?<datetime>.{19})",
            DateFrom = @"-dfrom\s(?<datetime>.{19})",
            ResponseIdRange = @"-rid\s\[(?<from>\d{1,3})-(?<to>\d{1,3})\]",
            IpUnique = @"-ipu\s(?<flag>(true|false))"
        };

        public List<Model> filtered = new List<Model>();

        public void FilterOut(string arguments, Model target)
        {
            ParseArguments(arguments);

            if (dateTimeFrom == null)
            {
                Console.WriteLine("Must have atleast date from!");
                return;
            }
            if (!(dateTimeTo != null && target.DateTime >= dateTimeFrom && target.DateTime <= dateTimeTo))
                return;
            if (!(target.StatusCode >= range[0] && target.StatusCode <= range[1]))
                return;
            if (unique && filtered.Where(w => w.Ip == target.Ip && w.DateTime.ToString("yyyy-MM-dd") == target.DateTime.ToString("yyyy-MM-dd")).Any())
                return;

            filtered.Add(target);
            Console.WriteLine($"Filtered record: {target.DateTime} : {target.Ip} : {target.StatusCode}");
        }

        private void ParseArguments(string arguments)
        {
            Match mDateTo = Regex.Match(arguments, args.DateTo);
            Match mDateFrom = Regex.Match(arguments, args.DateFrom);
            Match mResponseIdRange = Regex.Match(arguments, args.ResponseIdRange);
            Match mIpUnique = Regex.Match(arguments, args.IpUnique);

            if (mDateFrom.Success)
                dateTimeFrom = DateTime.ParseExact(mDateFrom.Groups["datetime"].Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            if (mDateTo.Success)
                dateTimeTo = DateTime.ParseExact(mDateTo.Groups["datetime"].Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            if (mResponseIdRange.Success)
            {
                range[0] = Int32.Parse(mResponseIdRange.Groups["from"].Value);
                range[1] = Int32.Parse(mResponseIdRange.Groups["to"].Value);
            }

            if (mIpUnique.Success && mIpUnique.Groups["flag"].Value.ToLower() == "true")
                unique = true;
        }
    }
}
