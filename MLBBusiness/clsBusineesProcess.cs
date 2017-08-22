using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLBData;
using clsModel;







namespace MLBBusiness
{
    class clsBusineesProcess
    {

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

