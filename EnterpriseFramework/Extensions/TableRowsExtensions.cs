using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Enterprise.Framework.Extensions
{
    public static class TableRowsExtensions
    {
        public static List<String> ToColumnList(this TableRows rows, String column)
        {
            var list = new List<String>();
            foreach (var row in rows)
            {
                if (!row[column].Equals(""))
                {
                    list.Add(row[column]);
                }
            }
            return list;
        }
    }
}
