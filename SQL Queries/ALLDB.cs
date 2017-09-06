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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

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
            timer1.Start();
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(7);
            loadESPNStandings();


            extractNextGames();

          
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

                case 3:

                    loadESPNStandings();


                    break;

                case 4:
                    dataGridView1.DataSource = clsConvert.convertHtml("http://www.espn.com/mlb/standings", 1, 1);
                    break;


            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void loadESPNStandings()
        {

            listBox1.Visible = false;
            dataGridView1.Visible = true;

            DataTable data3 = new DataTable();
            DataTable data4 = new DataTable();
            mlb_team theTeam= new mlb_team();


            data3 = clsConvert.convertHtml("http://www.espn.com/mlb/standings", 1, 1);
            data4 = clsConvert.convertHtml("http://www.espn.com/mlb/standings", 2, 1);


            data3.Merge(data4);


            foreach (DataRow row in data3.Rows)
            {
                //this.clsBusiness.updateTeam();
               
               theTeam = addTeamFromESPNStandins(row, theTeam);
               clsBusiness.upserTeam(theTeam);
                
            }

            dataGridView1.DataSource = data3;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            extractNextGames();

        }

        private void extractNextGames()
        {
            listBox1.Visible = true;
            dataGridView1.Visible = false;


            extractNextGames(int.Parse(numericUpDown1.Value.ToString()));

        }

        void extractNextGames(int qDays)
        {

            //First all games will be set as outdated, so just the loaded from page on next step will be set as updated.

            //clsBusiness.ExeStoredProcedure("[dbo].[sp_setGamesNotLastVersion]", );

            IDictionary<string, string> parametersDictionary = new Dictionary<string, string>();

            parametersDictionary.Add("@game_date_start", dateUrl(DateTime.Today));
            parametersDictionary.Add("@game_date_end", dateUrl(DateTime.Today.AddDays(qDays)));

            clsBusiness.ExeSPWithResults("sp_setGamesNotLastVersion", parametersDictionary);


            List<String> theList = new List<String>();

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


        mlb_team addTeamFromESPNStandins(DataRow row, mlb_team theTeam)
        {

         
            theTeam.team_name= row[0].ToString();
            theTeam.L10 = row["L10"].ToString();
            theTeam.win = int.Parse(row[1].ToString());
            theTeam.lost = int.Parse(row[2].ToString());
          theTeam.actualPosition = int.Parse(row[15].ToString());
            theTeam.division = row[14].ToString();
            theTeam.league = row[13].ToString();
            theTeam.last_update_date = DateTime.Now;

            return theTeam;
        }

        mlb_team_history addTeamHistoryFromTEAM(DataRow row, mlb_team theTeam)
        {

            mlb_team_history theTeamHistory= new mlb_team_history();

            theTeamHistory.id_team = theTeam.id_team;
            theTeamHistory.insert_date = DateTime.Today;
            theTeamHistory.L10 = theTeam.L10;
            theTeamHistory.lost = theTeamHistory.lost;
            theTeamHistory.position = theTeamHistory.position;
            theTeamHistory.win = theTeamHistory.win;
           

            return theTeamHistory;
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
                            theGame.game_pitcher_home_last3_ERA = float.Parse(row[15].ToString());
                        }
                        else
                        {
                            theGame.game_name_pitcher_away = rowText;
                            theGame.game_pitcher_away_ERA = float.Parse(row["ROAD"].ToString());
                            theGame.game_pitcher_away_last3_ERA = float.Parse(row[15].ToString());
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

        public string generateStandingsHomeText(int position, string division )
        {
           
            string txt1; string position_="";

            switch (position)
            {
                case 1: position_ = "1st";
                    break;
                    case 2:
                    position_ = "2nd";
                    break;
                case 3:
                    position_ = "3rd";
                    break;
                case 4:
                    position_ = "4th";
                    break;
                case 5:
                    position_ = "5th";
                    break;



            }

            txt1 = "" + position_ + " in " + division + "";


            return txt1;

        }

        public string generateStandingsAwayText(int position, string division)
        {
            string txt1;string position_ = "";

            switch (position)
            {
                case 1:
                    position_ = "1st";
                    break;
                case 2:
                    position_ = "2nd";
                    break;
                case 3:
                    position_ = "3rd";
                    break;
                case 4:
                    position_ = "4th";
                    break;
                case 5:
                    position_ = "5th";
                    break;



            }

            txt1 = "" + position_ + " in " + division + "";


            return txt1;

        }

        public List<String> addGame(string urlSource, string tableClass)
        {
            DataTable gamesTable = new DataTable();
            clsModel.mlb_game theGame = new mlb_game();
            List<String> theList = new List<String>();
            int contRowsGame = 0;
            mlb_team teamHome, teamAway;
            string teamHomeText; string teamAwayText;

            clsBusineesProcess theBusiness = new clsBusineesProcess();
            gamesTable = clsConvert.convertHtml("http://www.espn.com/mlb/probables/_/date/" + urlSource, 1, 0, tableClass);
            dataGridView1.DataSource = gamesTable;

            foreach (DataRow row in gamesTable.Rows)
            {

                contRowsGame++;               
                theGame = addGameFromESPNProbables(row, theGame, urlSource, contRowsGame);      

                //This next row means that is the correct row to have stored all fields and insert
                if (!Equals(theGame.game_name_pitcher_away, null))
                {

                    teamAway = theBusiness.insertTeam(theGame.game_name_team_away);
                    teamHome = theBusiness.insertTeam(theGame.game_name_team_home);
                    
                    theBusiness.insertPitcher(theGame.game_name_pitcher_home, float.Parse(theGame.game_pitcher_home_ERA.ToString()));
                    theBusiness.insertPitcher(theGame.game_name_pitcher_away, float.Parse(theGame.game_pitcher_away_ERA.ToString()));

                    if (teamAway.id_team != 0)
                    {
                        theGame.game_id_team_home = teamHome.id_team;
                    }

                    if (teamAway.id_team != 0)
                    {
                        theGame.game_id_team_away = teamAway.id_team;
                    }

                    theGame.game_team_away_L10 = teamAway.L10;
                    theGame.game_team_away_lost = teamAway.lost;
                    theGame.game_team_away_win = teamAway.win;
                    theGame.game_team_away_position = teamAway.actualPosition;
                  
                    theGame.game_team_home_lost = teamHome.lost;
                    theGame.game_team_home_win = teamHome.win;
                    theGame.game_team_home_L10 = teamHome.L10;
                    theGame.game_team_home_position = teamHome.actualPosition;

                    teamHomeText = generateStandingsHomeText(int.Parse(teamHome.actualPosition.ToString()), teamHome.division);
                    teamAwayText = generateStandingsAwayText(int.Parse(teamAway.actualPosition.ToString()), teamAway.division);

                    //theBusiness.insertGame(theGame);
                    theBusiness.upsertGame(theGame);

                   
                    
                    theList.Add("[" + theGame.game_date + "] :" + theGame.game_name_team_home + " " + teamHomeText +  " vs " + theGame.game_name_team_away + " " + teamAwayText + ". Pitchers: " + theGame.game_name_pitcher_home + ", ERA " + theGame.game_pitcher_home_ERA + " vs " + theGame.game_name_pitcher_away + ", ERA " + theGame.game_pitcher_away_ERA);

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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboChange(comboBox1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           

            loadESPNStandings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadESPNStandings();

            extractNextGames();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            dataGridView1.Visible = true;
            loadMLBSeries();
        }

        private void loadMLBSeries()
        {
            clsBusineesProcess clsBusiness = new clsBusineesProcess();
            DataTable dtMlbSeries = clsBusiness.ExeSPWithResults("select * from mlb_series_view order by qGames desc, startDate");
            DataTable dtMlBSeriesGame = new DataTable();
            IDictionary<string, string> theSpPms = new Dictionary<string, string>();


          
            
           foreach(DataRow row in dtMlbSeries.Rows)
            {
                theSpPms = new Dictionary<string, string>();

                theSpPms.Add("@serie_date_start", row["startDate"].ToString());
                theSpPms.Add("@serie_date_end", row["end_date"].ToString());
                theSpPms.Add("@id_game_team_home", row["game_id_team_home"].ToString());



                dtMlBSeriesGame = clsBusiness.ExeSPWithResults("[dbo].[sp_selectSeriesOfGames]", theSpPms);

                dataGridView1.DataSource = dtMlBSeriesGame;

                break;

            }


        }



        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                saveFile();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar guardar el archivo:" + ex.Message);
            }
        }


        void activeWorkSheet(Excel.Worksheet worksheet, int row, int col)
        {
            Excel.Range range = worksheet.UsedRange;

            int rows = range.Rows.Count;
            int columns = range.Columns.Count;
            
            Excel.Range activeCell = worksheet.Cells[row, col];
            activeCell.Select();
            


        
        }

        public void addExcelWorkSheet(Excel.Workbook excelWorkBook, int sheetNumber, int qGames, DateTime prmStartDate, DateTime prmEndDate)
        {
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets[sheetNumber];
            //excelWorkSheet.Name = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);

            excelWorkSheet.Select(Type.Missing);
            excelWorkSheet.Activate();
            string dateToDoc = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);

            clsBusineesProcess clsBusiness = new clsBusineesProcess();
            DataTable dtMlbSeries = clsBusiness.ExeSPWithResults("select * from mlb_series_view where startDate<='" + string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value) + "' and startDate >='" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "'  and qGames=" + qGames + " order by startDate");
            DataTable dtMlBSeriesGame = new DataTable();
            DataTable dtMlBSeriesGameHome = new DataTable();
            DataTable dtMlBSeriesGameAway = new DataTable();
            int idTeamHome, idTeamAway;
            string teamHomeText, teamAwayText;
            mlb_team teamHome = new mlb_team();
            mlb_team teamAway = new mlb_team();

            IDictionary<string, string> theSpPms = new Dictionary<string, string>();



            int i = 2; int j = 0; int cont = 0; int c = 0;
            foreach (DataRow row in dtMlbSeries.Rows)
            {

                theSpPms = new Dictionary<string, string>();

                theSpPms.Add("@serie_date_start", row["startDate"].ToString());
                theSpPms.Add("@serie_date_end", row["end_date"].ToString());
                theSpPms.Add("@id_game_team_home", row["game_id_team_home"].ToString());

                idTeamHome= int.Parse(row["game_id_team_home"].ToString());
                idTeamAway = int.Parse(row["game_id_team_away"].ToString());

                teamHome = clsBusiness.extractTeam(idTeamHome);
                teamAway = clsBusiness.extractTeam(idTeamAway);

                teamHomeText = generateStandingsHomeText(int.Parse(teamHome.actualPosition.ToString()), teamHome.division);
                teamAwayText = generateStandingsAwayText(int.Parse(teamAway.actualPosition.ToString()), teamAway.division);

                dtMlBSeriesGameHome = clsBusiness.ExeSPWithResults("[dbo].[sp_selectSeriesOfGamesHome]", theSpPms);

                dataGridView1.DataSource = dtMlBSeriesGameHome;

                dtMlBSeriesGameAway = clsBusiness.ExeSPWithResults("[dbo].[sp_selectSeriesOfGamesAway]", theSpPms);

                dataGridView1.DataSource = dtMlBSeriesGameAway;

              
                foreach (DataRow row2 in dtMlBSeriesGameHome.Rows)
                {
                    i += 3;
                    cont += 1;
                    j = 0; c = 0;
                    foreach (DataColumn dc in dtMlBSeriesGameHome.Columns)
                    {
                        j++;
                        c++;

                       if (j!=2)
                        { 
                        var field1 = row2[dc].ToString();
                        //var field2 = row2[dc].ToString();
                        excelWorkSheet.Cells[i, j] = field1;

                        var field2 = dtMlBSeriesGameAway.Rows[cont - 1][c - 1].ToString();
                        excelWorkSheet.Cells[i + 1, j] = field2;

                        }


                    }

                    excelWorkSheet.Cells[i, 11] = teamHomeText;
                    excelWorkSheet.Cells[i+1, 11] = teamAwayText;

                   

                }

              

                i += 1;

                activeWorkSheet(excelWorkSheet, i, 1);

                cont = 0;

            }
        }

        public void fillExcelV2(string templateFile, string outputFile, DataGridView dgv)
        {
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(templateFile);

            //Add a new worksheet to workbook with the Datatable name
            this.WindowState = FormWindowState.Minimized;

            addExcelWorkSheet(excelWorkBook, 1, 3, dateTimePicker1.Value, dateTimePicker2.Value);

            addExcelWorkSheet(excelWorkBook, 2, 4, dateTimePicker1.Value, dateTimePicker2.Value);


          
           
            try
            {
                excelWorkBook.SaveAs(outputFile);

                this.WindowState = FormWindowState.Normal;
               
                DialogResult dialogResult = MessageBox.Show("Excel file saved as  " + outputFile + ", would you like to close this app?", "Excel done", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to save the file: " + ex.Message);
            }
           




        }

        private void saveFile()
        {
            MessageBox.Show("Please wait a moment...");
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //saveFileDialog1.Filter = "excel files (*.xls)|*.xls|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.FileName = (@"MLB Auto House Report " + dateTimePicker1.Text + ".xlsx");

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dateToDoc = string.Format("{0:yyyy-MM-dd HH-mm-ss}", DateTime.Now);
                string path = (@"S:\MLBHouseReport\MLB Auto House Report " + dateToDoc + ".xlsx");

                try
                {
                    fillExcelV2(@"S:\MLBHouseReport\MLBBASE.xlsx", path, dataGridView1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    fillExcelV2(@"C:\MLBHouseReport\MLBBASE.xlsx", path, dataGridView1);
                }
                //fillExcelV2(@"C:\documents2017\desktop\ReportGameStats\DesktopC\bin\Debug\HOUSE REPORT BASE.xlsx", path + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".xlsx", dataGridView1);

                //File.WriteAllText(@saveFileDialog1.FileName + ".xls", rtOutput.Text);
                //MessageBox.Show("Archivo guardado con éxito!.");

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(7);
        }
    }
}
