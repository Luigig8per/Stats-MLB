using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Dbconnection
    {
        public SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123");

        public SqlConnection getConn()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        //public void ExeSPDinParameters(string SPName, List<SqlParameter> dbParameters)
        //{


        //    Guid newRecordGui = (Guid)SqlDatabase.ExecuteScalar(SPName, dbParameters);


        //    //foreach (SqlParameter sqlParameter in dbParameters)
        //    //    exe
        //}
        public int ExeNonQuery(SqlCommand cmd)
        {
            cmd.Connection = getConn();
            int rowsaffected = -1;

            rowsaffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsaffected;

        }
        public object ExeScalar(string insertString)
        {

            SqlCommand cmd = new SqlCommand(insertString, con);
            cmd.Connection = getConn();
            object obj = -1;
            obj = cmd.ExecuteScalar();
            con.Close();
            return obj;


        }

        public object ExeStoredProcedure(string storedProcedureName, int logIdUser, DateTime prmStartDate, DateTime prmEndDate)
        {

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    int rowsaffected = -1;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LogIdUser", 74);
                    cmd.Parameters.AddWithValue("@prmStartDate", prmStartDate.Date);
                    cmd.Parameters.AddWithValue("@prmEndDate", prmEndDate.Date.AddDays(1));
                    cmd.Parameters.AddWithValue("@prmBook", "");
                    cmd.Parameters.AddWithValue("@prmOffice", "");
                    cmd.Parameters.AddWithValue("@prmPlayer", "");
                    cmd.Parameters.AddWithValue("@prmLeague", "");
                    cmd.Parameters.AddWithValue("@prmGroupby", "");
                    cmd.Parameters.AddWithValue("@prmOrderby", "");

                    con.Open();
                    rowsaffected = cmd.ExecuteNonQuery();

                    return rowsaffected;
                }
            }

        }
       
  //      EXEC[dbo].[Report_Game_Statistic]
  //      @LogIdUser = 74,
  //@prmStartDate = N'2017-07-17',
  //@prmEndDate = N'2017-07-17',
  //@prmBook = '',
  //@prmOffice = '',
  //@prmPlayer = '',
  //@prmLeague = '',
  //@prmGroupby = 0,
  //@prmOrderby = 1

        public DataTable ExeSPWithResults(string storedProcedureName, List<string> sqlParameters)

        {

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    //foreach (string parameter in sqlParameters)
                    //{
                    //    cmd.Parameters.AddWithValue("", parameter);
                    //}

                    cmd.Parameters.AddWithValue("@LogIdUser",74);
                    cmd.Parameters.AddWithValue("@prmStartDate", "2017-07-17");
                    cmd.Parameters.AddWithValue("@prmEndDate", "2017-07-17");

                    cmd.Parameters.AddWithValue("@prmBook", "");
                    cmd.Parameters.AddWithValue("@prmOffice", "");
                    cmd.Parameters.AddWithValue("@prmPlayer", "");
                    cmd.Parameters.AddWithValue("@prmLeague", "");
                    cmd.Parameters.AddWithValue("@prmGroupby", "");
                    cmd.Parameters.AddWithValue("@prmOrderby", "");

                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    cmd.Connection = con;
                    dt.Load(sdr);
                    con.Close();
                    return dt;
                }
            }

        }

        public DataTable GetSPData(string spName, List<string> parametersList)
        {
            //make the list of the type that the method will be returning

            //make a connection string variable

            using ((con))
            {
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    con.Open();
                  
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@LogIdUser", 74);
                    cmd.Parameters.Add("@prmStartDate", SqlDbType.DateTime).Value = "7/20/2017";
                    cmd.Parameters.Add("@prmEndDate", SqlDbType.DateTime).Value = "7/20/2017";

                    cmd.Parameters.AddWithValue("@prmBook", "");
                    cmd.Parameters.AddWithValue("@prmOffice", "");
                    cmd.Parameters.AddWithValue("@prmPlayer", "");
                    cmd.Parameters.AddWithValue("@prmLeague", "");
                    cmd.Parameters.AddWithValue("@prmGroupby", 0);
                    cmd.Parameters.AddWithValue("@prmOrderby", 1);


                    //cmd.Parameters.AddWithValue("@LogIdUser", 74);
                    //cmd.Parameters.Add("@prmStartDate", SqlDbType.DateTime).Value = "7/20/2017";
                    //cmd.Parameters.Add("@prmEndDate", SqlDbType.DateTime).Value = "7/20/2017";

                    //cmd.Parameters.AddWithValue("@prmBook", "");
                    //cmd.Parameters.AddWithValue("@prmOffice", "");
                    //cmd.Parameters.AddWithValue("@prmPlayer", "");
                    //cmd.Parameters.AddWithValue("@prmLeague", "");
                    //cmd.Parameters.AddWithValue("@prmGroupby", 0);
                    //cmd.Parameters.AddWithValue("@prmOrderby", 1);

                    //instantiate SqlDataReader
                    SqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();

                    dt.Load(rdr);
                    con.Close();

                    return dt;

                }
               
            }
        }


        public DataTable ExeReader(SqlCommand cmd, List<string> sqlParameters)
        {
            cmd.Connection = getConn();

            cmd.Parameters.AddWithValue("@LogIdUser", 74);
            cmd.Parameters.AddWithValue("@prmStartDate", "2017-07-17");
            cmd.Parameters.AddWithValue("@prmEndDate", "2017-07-17");

            cmd.Parameters.AddWithValue("@prmBook", "");
            cmd.Parameters.AddWithValue("@prmOffice", "");
            cmd.Parameters.AddWithValue("@prmPlayer", "");
            cmd.Parameters.AddWithValue("@prmLeague", "");
            cmd.Parameters.AddWithValue("@prmGroupby", "");
            cmd.Parameters.AddWithValue("@prmOrderby", "");

            SqlDataReader sdr;
            DataTable dt = new DataTable();

            sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            return dt;

        }


        public DataTable ExeReader(SqlCommand cmd)
        {
            cmd.Connection = getConn();
            SqlDataReader sdr;
            DataTable dt = new DataTable();

            sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            return dt;

        }

        public DataTable insertData(string spName, List<string> parametersList)
        {
            DataTable res = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Report_Game_Statistic";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@LogIdUser", 74);
                cmd.Parameters.AddWithValue("@prmStartDate", "7/20/2017");
                cmd.Parameters.AddWithValue("@prmEndDate", "7/20/2017");

                cmd.Parameters.AddWithValue("@prmBook", "");
                cmd.Parameters.AddWithValue("@prmOffice", "");
                cmd.Parameters.AddWithValue("@prmPlayer", "");
                cmd.Parameters.AddWithValue("@prmLeague", "");
                cmd.Parameters.AddWithValue("@prmGroupby", 0);
                cmd.Parameters.AddWithValue("@prmOrderby", 1);

                try
                {
                    con.Open();
                   
                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();
                   
                }
            }
            return res;
        }


        public DataTable insertData(string spName,int logIdUser, string prmStartDate, string prmEndDate)
        {
            DataTable res = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Report_Game_Statistic";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@LogIdUser", 74);
                //cmd.Parameters.AddWithValue("@prmStartDate", "7/20/2017");
                //cmd.Parameters.AddWithValue("@prmEndDate", "7/20/2017");

                cmd.Parameters.AddWithValue("@prmStartDate", prmStartDate);
                cmd.Parameters.AddWithValue("@prmEndDate", prmEndDate);

                cmd.Parameters.AddWithValue("@prmBook", "");
                cmd.Parameters.AddWithValue("@prmOffice", "");
                cmd.Parameters.AddWithValue("@prmPlayer", "");
                cmd.Parameters.AddWithValue("@prmLeague", "");
                cmd.Parameters.AddWithValue("@prmGroupby", 0);
                cmd.Parameters.AddWithValue("@prmOrderby", 1);

                try
                {
                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();

                }
            }
            return res;
        }

        public DataTable extractDataSP(string spName, int logIdUser, string prmStartDate, string prmEndDate)
        {
            DataTable res = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Report_Game_Statistic";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@LogIdUser", 74);
                //cmd.Parameters.AddWithValue("@prmStartDate", "7/20/2017");
                //cmd.Parameters.AddWithValue("@prmEndDate", "7/20/2017");

                cmd.Parameters.AddWithValue("@prmStartDate", prmStartDate);
                cmd.Parameters.AddWithValue("@prmEndDate", prmEndDate);

                cmd.Parameters.AddWithValue("@prmBook", "");
                cmd.Parameters.AddWithValue("@prmOffice", "");
                cmd.Parameters.AddWithValue("@prmPlayer", "");
                cmd.Parameters.AddWithValue("@prmLeague", "");
                cmd.Parameters.AddWithValue("@prmGroupby", 0);
                cmd.Parameters.AddWithValue("@prmOrderby", 1);

                try
                {
                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();

                }
            }
            return res;
        }

        public DataTable extractDataSP(string spName)
        {
            DataTable res = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                try
                {
                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();

                }
            }
            return res;
        }

        public DataTable extractDataSP(string spName, string idSport)
        {
            DataTable res = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@prmIdSport", idSport);

                try
                {
                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();

                }
            }
            return res;
        }


        public DataTable getGameStats(string spName, int logIdUser, string League,  string prmStartDate, string prmEndDate)
        {
            DataTable res = new DataTable();
            using (con = new SqlConnection("Data Source=10.10.10.30;Initial Catalog=DGSDATA;User ID=luisma;Password=luis123"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Report_Game_Statistic";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@LogIdUser", 74);
                //cmd.Parameters.AddWithValue("@prmStartDate", "7/20/2017");
                //cmd.Parameters.AddWithValue("@prmEndDate", "7/20/2017");

                cmd.Parameters.AddWithValue("@prmStartDate", prmStartDate);
                cmd.Parameters.AddWithValue("@prmEndDate", prmEndDate);

                cmd.Parameters.AddWithValue("@prmBook", "");
                cmd.Parameters.AddWithValue("@prmOffice", "");
                cmd.Parameters.AddWithValue("@prmPlayer", "");
                cmd.Parameters.AddWithValue("@prmLeague", League);
                cmd.Parameters.AddWithValue("@prmGroupby", 0);
                cmd.Parameters.AddWithValue("@prmOrderby", 1);

                try
                {
                    con.Open();

                    SqlDataReader sdr;
                    DataTable dt = new DataTable();

                    sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    con.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();

                }
            }
            return res;
        }


    }

   
}
