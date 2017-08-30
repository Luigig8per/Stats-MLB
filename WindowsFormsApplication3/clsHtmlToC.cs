using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class clsHtmlToC
    {
        public DataTable convertHtml(string webpage, int League, int extraColumns, string cls)
        {
            DataTable res= new DataTable();

            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(webpage);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            
            //switch (cls)
            //{
            //    //case ;

            //}

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("(//table[@class='" + cls + "'])[" + League + "]")
                        .Descendants("tr")
                        //.Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();
          
                
            switch (extraColumns)
            {
                case 0:
                   res= table.ToDataTable();
                    break;


                case 1:
                   res= table.ToDataTableWithPosition(League);
                    break;

            }

            return res;
            
        }

        public DataTable convertHtml(string webpage, int League, int extraColumns)
        {
            DataTable res = new DataTable();

            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(webpage);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            //switch (cls)
            //{
            //    //case ;

            //}

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("(//table[@class='standings has-team-logos'])[" + League + "]")
                        .Descendants("tr")
                        //.Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();


            switch (extraColumns)
            {
                case 0:
                    res = table.ToDataTable();
                    break;


                case 1:
                    
                    res = table.ToDataTableWithPosition(League);

                  

                    break;

            }

            return res;

        }


    }
}
