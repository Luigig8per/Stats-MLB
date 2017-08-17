using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    static class ListExtensions
    {
        public static DataTable ToDataTable(this List<List<string>> list)
        {
            DataTable tmp = new DataTable();

            tmp.Columns.Add("Team");
            tmp.Columns.Add("Win");
            tmp.Columns.Add("Lost");
            tmp.Columns.Add("PCT");
            tmp.Columns.Add("GB");
            tmp.Columns.Add("HOME");
            tmp.Columns.Add("ROAD");
            tmp.Columns.Add("RS");
            tmp.Columns.Add("RA");
            tmp.Columns.Add("DIFF");
            tmp.Columns.Add("STRK");
            tmp.Columns.Add("L10");
           
           


            foreach (List<string> row in list)
            {
                tmp.Rows.Add(row.ToArray());
            }
            return tmp;
        }


    }
}
