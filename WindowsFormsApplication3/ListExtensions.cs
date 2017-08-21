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
        public static DataTable ToDataTableWithPosition(this List<List<string>> list)
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
            tmp.Columns.Add("League");
            tmp.Columns.Add("Subgroup");
            tmp.Columns.Add("Position");
            tmp.Columns.Add("");
            tmp.Columns.Add("");
            tmp.Columns.Add("");
            tmp.Columns.Add("");

            int i = 0;
            int j = 1;
            string leagueName = "";
            string area = "";

            foreach (List<string> row in list)
            {

                if (i >= 5)
                { 
                    i = 0;
                j += 1;
                }

                i += 1;
                

                switch(j)
                { 

                    case 1:
                        area = "EAST";
                        break;

                case 2:
                        area = "CENTRAL";
                        break;
                case 3:
                        area = "WEST";
                        break;

                }

               



                row.Add(i.ToString());
                row.Add(area);
                row.Add(i.ToString());

                tmp.Rows.Add(row.ToArray());
                
            }
            return tmp;
        }

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
