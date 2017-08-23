using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLBData;
using clsModel;


namespace MLBBusiness
{
    public class clsBusineesProcess
    {

        public void insertGame(mlb_game theGame)
        {
            int res=0;


            using (DonBestEntities context = new DonBestEntities())

            {
                Console.WriteLine("Home team = " + theGame.game_name_team_home);
                Console.WriteLine("Away team = " + theGame.game_name_team_away);
                Console.WriteLine("Home pitcher = " + theGame.game_name_pitcher_home);
                Console.WriteLine("Away pitcher = " + theGame.game_name_pitcher_away);

                try
                {

                    context.mlb_game.Add(theGame);
                    res =     context.SaveChanges();
                }
                catch (Exception ex)

                {
                    Console.WriteLine("Error" + ex.Message);
                   

                }

                Console.WriteLine(res);




            }
        }

        public void selectTeam()
        {
            using (DonBestEntities context = new DonBestEntities())
            {

                mlb_game theMlbGame = context.mlb_game.FirstOrDefault(r => r.game_id == 1);

                Console.WriteLine(theMlbGame.game_date);
                Console.Read();


            }
        }

        public void updateTeam()
        {
            using (DonBestEntities context = new DonBestEntities())

            {
                mlb_game theMlbGame = context.mlb_game.FirstOrDefault(r => r.game_id == 1);

                theMlbGame.game_date = DateTime.Today.AddDays(1);

                context.SaveChanges();


            }
        }
        public void insertNewTeam()
        {
            using (DonBestEntities context = new DonBestEntities())
            {



                mlb_game theMlbGame = new mlb_game


                {
                    game_date = DateTime.Now

                };

                context.mlb_game.Add(theMlbGame);

                context.SaveChanges();

                     

                   
                    
               }

                


            }

        public void deleteTeam()
        {
            using (DonBestEntities context = new DonBestEntities())
            {
                mlb_game theMlbGame = context.mlb_game.FirstOrDefault(r => r.game_id == 1);
                context.mlb_game.Remove(theMlbGame);


                context.SaveChanges();

               

            }
        }


        }


    }

