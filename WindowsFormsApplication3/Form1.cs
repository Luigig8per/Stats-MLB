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

                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/standings/_/group/overall",1,0, "standings has - team - logos");
                    break;

                case 1:
                    DataTable data1 = new DataTable();
                    DataTable data2 = new DataTable();

                 


                    data1= clsConvert.convertHtml("http://www.espn.com/mlb/standings",1,1, "standings has - team - logos");
                    data2= clsConvert.convertHtml("http://www.espn.com/mlb/standings",2,1, "standings has - team - logos");

                   
                        data1.Merge(data2);


                    dataGridView1.DataSource = data1;
                    break;

                case 2:

                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/team/schedule/_/name/cle", 1, 0, "standings has-team-logos");
                    break;



            }

           



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/team/schedule/_/name/mia", 2, 0, "schedule has-team-logos align-left");
            //Original for this should be: /*http://www.espn.com/mlb/schedule/_/date/20170821 its working, but not show time on extraction
            ////this one is usefull for extract serie quantity
            //dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/team/schedule/_/name/cle", 1, 0, "tablehead");

            //Next one works very good, but its by teams.

            dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/schedule/_/date/20170822", 1, 1, "schedule has-team-logos align-left");

            //clsConvert.convertHtml("http://www.espn.com/mlb/team/schedule/_/name/mia", 1, 0, "http://www.espn.com/mlb/schedule");

            //clsConvert.convertHtml("http://www.espn.com/mlb/schedule", 1, 0, "schedule has - team - logos align - left");

        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        //Extract Pitcher stats:

        //http://m.mlb.com/player/607074/carlos-rodon
        //http://m.mlb.com/player/622072/alex-wood
        //http://m.mlb.com/player/489119/wade-miley
        //http://m.mlb.com/player/605182/mike-clevinger


        ////http://www.espn.com/mlb/team/schedule FROM schedule: http://www.espn.com/mlb/schedule/_/date/20170821

        ////http://m.mlb.com/player/607074/carlos-rodon from  http://mlb.mlb.com/mlb/schedule/index.jsp#date=08/21/2017 

        //http://www.covers.com/pageLoader/pageLoader.aspx?page=/data/mlb/players/player116570.html from http://www.covers.com/sports/mlb/matchups

            
            dataGridView1.DataSource = clsConvert.convertHtml("http://m.mlb.com/player/607074/carlos-rodon", 1, 0, "tablehead");

        //http://www.covers.com/pageLoader/pageLoader.aspx?page=/data/mlb/players/player114276.html


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ERA();
    
           


        }

        private float ERA()
        {
            DataTable dTpitchers = new DataTable();
            string sEra = "";


            dTpitchers = clsConvert.convertHtml("http://www.espn.com/mlb/player/_/id/31803/tim-melville", 2, 0, "tablehead");

            dataGridView1.DataSource = dTpitchers; 
          sEra = dTpitchers.Rows[0][17].ToString();


            MessageBox.Show(sEra);

            return float.Parse(sEra);
        }
    }
}
