using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLBData;
using clsModel;
using System.Data.Entity;

namespace MLBBusiness
{
    public class clsBusineesProcess
    {

        public void insertGame(mlb_game theGame)
        {
            int res=0;

            using (DonBestEntities context = new DonBestEntities())

            {

                Console.WriteLine("TO INSERT" );
                Console.WriteLine("Home team = " + theGame.game_name_team_home);
                Console.WriteLine("Away team = " + theGame.game_name_team_away);
                Console.WriteLine("Home pitcher = " + theGame.game_name_pitcher_home);
                Console.WriteLine("Away pitcher = " + theGame.game_name_pitcher_away);

                try
                {

                    context.mlb_game.Add(theGame);
                    res = context.SaveChanges();
                }
                catch (Exception ex)

                {
                    Console.WriteLine("Error" + ex.Message);


                }

                Console.WriteLine(res);

            }
        }

        public void insertTeam(string teamName)
        {
            mlb_team theTeam = new mlb_team();

            theTeam.insert_date = DateTime.Now;
            theTeam.team_name = teamName;
            
            int res = 0;

            using (DonBestEntities context = new DonBestEntities())

            {

                var L2EQuery = context.mlb_team.Where(t => t.team_name == theTeam.team_name);


                var teamFound = L2EQuery.FirstOrDefault<mlb_team>();

                if (teamFound == null)
                {
                    try
                    {

                        context.mlb_team.Add(theTeam);
                        res = context.SaveChanges();
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Error" + ex.Message);


                    }

                    Console.WriteLine(res);

                }

            }
        }



        public void updatetGame( mlb_game gameFound, mlb_game theGame,  DonBestEntities context)
        {
            int res = 0; int countChanges = 0;

            try
            {
                if (gameFound.game_name_pitcher_away != theGame.game_name_pitcher_away)
                { 
                    gameFound.game_name_pitcher_away =theGame.game_name_pitcher_away;
                countChanges++;
                }

                if (gameFound.game_name_pitcher_home != theGame.game_name_pitcher_home)
                { 
                    gameFound.game_name_pitcher_home = theGame.game_name_pitcher_home;
                    countChanges++;
                }

                if (gameFound.game_name_pitcher_home != theGame.game_name_pitcher_home)
                {
                    gameFound.game_name_pitcher_home = theGame.game_name_pitcher_home;
                    countChanges++;
                }

                if (gameFound.game_name_team_away != theGame.game_name_team_away)
                { 
                gameFound.game_name_team_away = theGame.game_name_team_away;
                    countChanges++;
                }

                if (gameFound.game_name_team_home != theGame.game_name_team_home)
                { 
                gameFound.game_name_team_home = theGame.game_name_team_home;
                    countChanges++;
                }

                if (gameFound.game_pitcher_away_ERA != theGame.game_pitcher_away_ERA)
                {
                    gameFound.game_pitcher_away_ERA = theGame.game_pitcher_away_ERA;
                    countChanges++;
                }

                if (gameFound.game_pitcher_home_ERA != theGame.game_pitcher_home_ERA)
                {
                    gameFound.game_pitcher_away_ERA = theGame.game_pitcher_away_ERA;
                    countChanges++;
                }

               if (countChanges>0)
                { 
                gameFound.last_update_date = DateTime.Now;

                gameFound.updated = true;
                }
                //context.mlb_game.

            }
            catch (Exception ex)

            {
                Console.WriteLine("Error" + ex.Message);


            }


            using (context = new DonBestEntities())

            {
                Console.WriteLine("TO UPDATE:");
                Console.WriteLine("Home team = " + theGame.game_name_team_home);
                Console.WriteLine("Away team = " + theGame.game_name_team_away);
                Console.WriteLine("Home pitcher = " + theGame.game_name_pitcher_home);
                Console.WriteLine("Away pitcher = " + theGame.game_name_pitcher_away);


                context.Entry(gameFound).State = System.Data.Entity.EntityState.Modified;
                res = context.SaveChanges();

                Console.WriteLine(res);

            }
        }

        public void queryExists()
        {

        }

        public void upsertGame(mlb_game theGame)
        {
            int res = 0;

            using (DonBestEntities context = new DonBestEntities())

            {

                Console.WriteLine("Home team = " + theGame.game_name_team_home);
                Console.WriteLine("Away team = " + theGame.game_name_team_away);
                Console.WriteLine("Home pitcher = " + theGame.game_name_pitcher_home);
                Console.WriteLine("Away pitcher = " + theGame.game_name_pitcher_away);

                try
                {

                    //If game and date is same, and pitchers change, need to do update.
                    //if GAME AND DATE IS SAME, 
                    //if game date same, 
                    //var L2EQuery = context.mlb_game.Where(g => g.game_date == theGame.game_date && g.game_name_pitcher_away == theGame.game_name_pitcher_away && g.game_name_pitcher_home == theGame.game_name_pitcher_home && g.game_pitcher_away_ERA == theGame.game_pitcher_away_ERA && g.game_pitcher_home_ERA == theGame.game_pitcher_home_ERA );

                    var L2EQuery = context.mlb_game.Where(g => g.game_date == theGame.game_date && g.game_name_team_home == theGame.game_name_team_home && g.game_name_team_away == theGame.game_name_team_away);


                    var gameFound = L2EQuery.FirstOrDefault<mlb_game>();

                    if (gameFound == null)
                    {

                         
                        insertGame(theGame);

                    

                    }
                    else

                    {
                        updatetGame(gameFound, theGame, context );

                    }

                    //if (gameFound i)  

                    //context.mlb_game.
                    res = context.SaveChanges();
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

