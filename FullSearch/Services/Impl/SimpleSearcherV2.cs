using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearch.Services.Impl
{
    public class SimpleSearcherV2
    {
        public void SearchV1(string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                if (item.Contains(word, StringComparison.InvariantCultureIgnoreCase))
                    Console.WriteLine(PrettyMatchV1(word, item));
            }
        }

        private string PrettyMatchV1(string word, string text)
        {
            int pos = text.IndexOf(word);
            var start = Math.Max(0, pos - 50);
            int end = Math.Min(start + 100, text.Length - 1);
            return $"{(start == 0 ? "" : "...")}{text.Substring(start, end - start)}{(end == text.Length - 1 ? "" : "...")}";
        }
    }
}
