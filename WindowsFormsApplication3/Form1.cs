using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        clsHtmlToC clsConvert = new clsHtmlToC();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

         
            switch (comboBox1.SelectedIndex)

            { 

                case 0:

                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/standings/_/group/overall",1);
                    break;

                case 1:
                    DataTable data1 = new DataTable();
                    DataTable data2 = new DataTable();

                    int cont = 0;


                    data1= clsConvert.convertHtml("http://www.espn.com/mlb/standings",1);
                    data2= clsConvert.convertHtml("http://www.espn.com/mlb/standings",2);

                    
                    

                  

                    //for (int i=1; i<=3; i++)
                    //{
                    //    for (int j=1; j<=5; j++)
                    //    {
                    //        data1[1,3].
                    //    }
                    //}

                        data1.Merge(data2);


                    dataGridView1.DataSource = data1;
                    break;



            }

           



        }
    }
}
