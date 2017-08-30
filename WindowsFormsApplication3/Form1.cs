using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLBBusiness;
using clsModel;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        clsHtmlToC clsConvert = new clsHtmlToC();
        clsBusineesProcess clsBusiness = new clsBusineesProcess();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            extractNextGames(int.Parse(numericUpDown1.Value.ToString()));





        }

        public void comboChange(ComboBox comboBox1)
        {


            switch (comboBox1.SelectedIndex)

            {

                case 0:

                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/standings/_/group/overall", 1, 0, "standings has - team - logos");
                    break;

                case 1:
                    DataTable data1 = new DataTable();
                    DataTable data2 = new DataTable();




                    data1 = clsConvert.convertHtml("http://www.espn.com/mlb/standings", 1, 1, "standings has - team - logos");
                    data2 = clsConvert.convertHtml("http://www.espn.com/mlb/standings", 2, 1, "standings has - team - logos");


                    data1.Merge(data2);


                    dataGridView1.DataSource = data1;
                    break;

                case 2:

                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/team/schedule/_/name/cle", 1, 0, "standings has-team-logos");
                    break;



            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            extractNextGames(int.Parse(numericUpDown1.Value.ToString()));



        }

        void extractNextGames(int qDays)
        {
            List<String> theList = new List<String>();

            //addGame("http://www.espn.com/mlb/schedule/_/date/20170823");


            theList = addGame(dateUrl(DateTime.Today), "tablehead");

            for (int i = 1; i <= qDays; i++)
            {
                theList.AddRange(addGame(dateUrl(DateTime.Today.AddDays(i)), "tablehead"));
            }

            listBox1.DataSource = theList;
        }

        mlb_game addGameFrom(DataRow row, mlb_game theGame)
        {
            string pitchers = "";
            int indexNum;
            //   
            theGame.game_name_team_home = row["Team"].ToString();
            theGame.game_name_team_away = row["Win"].ToString();
            theGame.game_date = DateTime.Today;
            //theGame.game_serie_id = 1;
            //theGame.game_number = 20;
            //theGame.game_id_team_away = 20;
            //theGame.game_id_team_home = 20;


            pitchers = row["GB"].ToString();
            indexNum = pitchers.IndexOf("vs");

            theGame.game_name_pitcher_home = pitchers.Substring(0, indexNum);
            theGame.game_name_pitcher_away = pitchers.Substring(indexNum + 3, pitchers.Length - indexNum - 3);

            return theGame;
        }


        DateTime convert(string date)
        {
            //original is 3:05 PM ET

            //string pattern = "h:mm tt zzz";

            DateTime dt = Convert.ToDateTime(date);

            return dt;

            //DateTime gameTime = DateTime.Today;

            //gameTime.
            //game= new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, )



        }
        mlb_game addGameFromESPNProbables(DataRow row, mlb_game theGame, string theDate, int countRowsGame)
        {
            //string pitchers = "", lineText = "";
            int indexNum;
            string rowText = "";


            rowText = row["Team"].ToString();

            indexNum = rowText.IndexOf(" at ");


            if (rowText.Contains(" at "))
            {
                theDate += " " + (row["Win"].ToString().Substring(0, 8));
                theGame.game_date = dateUrl(theDate);

                //timeFromText();

                //dateUrl(thedATE);
                theGame.game_name_team_home = rowText.Substring(0, indexNum);
               


                theGame.game_name_team_away = rowText.Substring(indexNum + 4, rowText.Length - indexNum - 4);
                //theGame
                ////theGame.game_date = row["Win"].ToString();

                //theGame.game_date= row["Team"].ToString();

                //theGame.game_date = row["Win"].ToString();

                //theGame.game_date = convert(row["Win"].ToString());
              
                theGame.insert_date = DateTime.Now;
                theGame.updated = false;
            }


            else
            {

                if (countRowsGame == 4 || countRowsGame == 5)
                {
                    if (rowText.Contains("(R)") || rowText.Contains("(L)"))
                    {
                        if (Equals(theGame.game_name_pitcher_home, null))
                        {
                            theGame.game_name_pitcher_home = rowText;
                            theGame.game_pitcher_home_ERA = float.Parse(row["ROAD"].ToString());
                        }
                        else
                        {
                            theGame.game_name_pitcher_away = rowText;
                            theGame.game_pitcher_away_ERA = float.Parse(row["ROAD"].ToString());
                        }





                    }



                    else

                    {
                        theGame.game_name_pitcher_home = "UNDEFINED";
                        theGame.game_name_pitcher_away = "UNDEFINED";
                    }



                }

                else
                {

                }
            }


            //indexNum = pitchers.IndexOf("vs");

            //theGame.game_name_pitcher_home = pitchers.Substring(0, indexNum);
            //theGame.game_name_pitcher_away = pitchers.Substring(indexNum + 3, pitchers.Length - indexNum - 3);

            return theGame;
        }

        string dateUrl(DateTime day)
        {

            string datePage = "";



            datePage = String.Format("{0:yyyyMMdd}", day);

            return datePage;

            //http://www.espn.com/mlb/probables/_/date/20170824
        }

        //DateTime timeFromText(string hour)
        //{
        //    DateTime datePage = new DateTime();



        //    datePage = DateTime.ParseExact(hour, "H:mm tt",
        //                           System.Globalization.CultureInfo.InvariantCulture);

        //    return datePage;
        //}

        DateTime dateUrl(string day)
        {
            DateTime datePage = new DateTime();

            if (day.Length > 10)
            {
                if (day.Substring(16, 1) == " ")
                {
                    day = day.Substring(0, 16);
                }
            }




            datePage = DateTime.ParseExact(day, "yyyyMMdd h:mm tt",
                                   System.Globalization.CultureInfo.InvariantCulture);

            return datePage;

        }



        public List<String> addGame(string urlSource, string tableClass)
        {
            DataTable gamesTable = new DataTable();
            clsModel.mlb_game theGame = new mlb_game();
            List<String> theList = new List<String>();
            int contRowsGame = 0;
            int idTeamHome, idTeamAway;


            clsBusineesProcess theBusiness = new clsBusineesProcess();
            gamesTable = clsConvert.convertHtml("http://www.espn.com/mlb/probables/_/date/" + urlSource, 1, 0, tableClass);
            dataGridView1.DataSource = gamesTable;

            foreach (DataRow row in gamesTable.Rows)
            {


                contRowsGame++;
                //theGame.game_date = 
                theGame = addGameFromESPNProbables(row, theGame, urlSource, contRowsGame);

              

                //if (idPitcherHome != 0)
                //{
                 
                //}

                //if (idPitcherAway!=0)
                //{

                //}



                if (!Equals(theGame.game_name_pitcher_away, null))
                {

                    idTeamHome = theBusiness.insertTeam(theGame.game_name_team_away);
                    idTeamAway = theBusiness.insertTeam(theGame.game_name_team_home);
                    
                    theBusiness.insertPitcher(theGame.game_name_pitcher_home, float.Parse(theGame.game_pitcher_home_ERA.ToString()));
                    theBusiness.insertPitcher(theGame.game_name_pitcher_away, float.Parse(theGame.game_pitcher_away_ERA.ToString()));

                    if (idTeamHome != 0)
                    {
                        theGame.game_id_team_home = idTeamHome;
                    }

                    if (idTeamAway != 0)
                    {
                        theGame.game_id_team_away = idTeamAway;
                    }

                    //theBusiness.insertGame(theGame);
                    theBusiness.upsertGame(theGame);

                    
                    theList.Add("[" + theGame.game_date + "] :" + theGame.game_name_team_home + " vs " + theGame.game_name_team_away + ". Pitchers: " + theGame.game_name_pitcher_home + ", ERA " + theGame.game_pitcher_home_ERA + " vs " + theGame.game_name_pitcher_away + ", ERA " + theGame.game_pitcher_away_ERA);



                    theGame = new mlb_game();
                    contRowsGame = 0;
                }
                //if (theGame.game_pitcher_away_ERA != "")
                //{ 
                //    clsBusiness.insertGame(theGame);
                //    theGame = new mlb_game();

                //}
            }

            return theList;




        }

        public List<string> upsertGame(string urlSource, string tableClass)
        {
            DataTable gamesTable = new DataTable();
            clsModel.mlb_game theGame = new mlb_game();
            List<String> theList = new List<String>();
            int countRowsGame = 0;
            clsBusineesProcess theBusiness = new clsBusineesProcess();
            gamesTable = clsConvert.convertHtml("http://www.espn.com/mlb/probables/_/date/" + urlSource, 1, 0, tableClass);
            dataGridView1.DataSource = gamesTable;

            foreach (DataRow row in gamesTable.Rows)
            {
                countRowsGame++;
                theGame = addGameFromESPNProbables(row, theGame, urlSource, countRowsGame);

                if (!Equals(theGame.game_name_pitcher_away, null))
                {

                    theBusiness.insertGame(theGame);
                    theList.Add("[" + theGame.game_date + "] :" + theGame.game_name_team_home + " vs " + theGame.game_name_team_away + ". Pitchers: " + theGame.game_name_pitcher_home + ", ERA " + theGame.game_pitcher_home_ERA + " vs " + theGame.game_name_pitcher_away + ", ERA " + theGame.game_pitcher_away_ERA);



                    theGame = new mlb_game();

                }
                //if (theGame.game_pitcher_away_ERA != "")
                //{ 
                //    clsBusiness.insertGame(theGame);
                //    theGame = new mlb_game();

                //}
            }

            return theList;

        }

        public int extractPitcher(string urlPitcher)
        {
            DataTable pitchersTable = new DataTable();
            //pitchersTable = clsConvert.convertHtml() 

            return 0;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {




        }

        public void extractPitcherStats()
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

        private DataTable extractPitchersStats()
        {
            DataTable dTpitchers = clsConvert.convertHtml("http://www.rotowire.com/baseball/player_stats.htm?pos=P", 1, 1, "tablesorter headerfollows footballproj-table tablesorter-default hasStickyHeaders");

            return dTpitchers;

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

        public void insertGame()
        {
            //MLBBusiness.
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
