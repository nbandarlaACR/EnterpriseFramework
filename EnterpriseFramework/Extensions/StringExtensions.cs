using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string FixApiResponseString(this string input)
        {
            input = input.Replace("\\", string.Empty);
            input = input.Trim('"');
            return input;
        }

        public static List<string> SplitAtOccurence(this string input, char separator, int occurence)
        {
            var parts = input.Split(separator);
            var partlist = new List<string>();
            var result = new List<string>();
            for (int i = 0; i < parts.Length; i++)
            {
                if (partlist.Count == occurence)
                {
                    result.Add(string.Join(separator.ToString(), partlist));
                    partlist.Clear();
                }
                partlist.Add(parts[i]);
                if (i == parts.Length - 1) result.Add(string.Join(separator.ToString(), partlist)); // if no more parts, add the rest
            }
            return result;
        }
    }
}
