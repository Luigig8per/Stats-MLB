﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLBData;
using clsModel;
using System.Data.Entity;
using System.Data;
using MLBBusiness;

namespace MLBBusiness
{
    public class clsBusineesProcess
    {

        public void insertGame(mlb_game theGame)
        {
            int res=0;

            using (StatsEntities context = new StatsEntities())

            {

               

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

        public mlb_team insertTeam(string teamName)
        {
            mlb_team theTeam = new mlb_team();

            theTeam.insert_date = DateTime.Now;
            theTeam.team_name = teamName;

            mlb_team teamFound;
           

            using (StatsEntities context = new StatsEntities())

            {

                var L2EQuery = context.mlb_team.Where(t => t.team_name == theTeam.team_name);


               teamFound = L2EQuery.FirstOrDefault<mlb_team>();
             
                if (teamFound == null)
                {
                    try
                    {

                        context.mlb_team.Add(theTeam);
                        context.SaveChanges();
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Error" + ex.Message);


                    }

                    

                }
               

            }

            return teamFound;
        }

        public mlb_team extractTeam(int teamId)
        {
                     
           mlb_team teamFound;


            using (StatsEntities context = new StatsEntities())

            {

                var L2EQuery = context.mlb_team.Where(t => t.id_team == teamId);


                teamFound = L2EQuery.FirstOrDefault<mlb_team>();

              
            }

            return teamFound;
        }


        public int insertTeamHistory(mlb_team entity)
        {
            mlb_team_history theEntityHistory = new mlb_team_history();

            theEntityHistory.insert_date = DateTime.Now;
            theEntityHistory.id_team = entity.id_team;
            theEntityHistory.L10 = entity.L10;
            theEntityHistory.lost = int.Parse(entity.lost.ToString());
            theEntityHistory.win = int.Parse(entity.win.ToString());
            theEntityHistory.position = int.Parse(entity.actualPosition.ToString());



            int res = 0;
            int id_team_history = 0;

            using (StatsEntities context = new StatsEntities())

            {

                var L2EQuery = context.mlb_team_history.Where(t => t.id_team == entity.id_team && t.insert_date >= DateTime.Today && t.insert_date <= System.Data.Entity.DbFunctions.AddDays(DateTime.Today, 1));

                var teamFound = L2EQuery.FirstOrDefault<mlb_team_history>();

                if (teamFound == null)
                {
                    try
                    {

                        context.mlb_team_history.Add(theEntityHistory);
                        res = context.SaveChanges();
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Error" + ex.Message);


                    }

                    Console.WriteLine(res);

                }
                else
                {
                    id_team_history = teamFound.id_team_history;
                }

            }

            return id_team_history;
        }


        public int insertGameHistory(mlb_game entity, int idGame)
        {
            clsGeneric genericClass = new clsGeneric();
            mlb_game_history theEntityHistory = new mlb_game_history();

            genericClass.transferPropertiesToAnotherClass(entity, theEntityHistory);
           

            theEntityHistory.insert_date = DateTime.Now;
            theEntityHistory.id_game = idGame;

            int res = 0;
            int id_team_history = 0;

            using (StatsEntities context = new StatsEntities())

            {

              
                {
                    try
                    {

                        context.mlb_game_history.Add(theEntityHistory);
                        res = context.SaveChanges();
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Error" + ex.Message);


                    }

                    Console.WriteLine(res);

                }
               

            }

            return id_team_history;
        }


        public int insertPitcher(string teamName, float ERA)
        {
            mlb_pitcher thePitcher = new mlb_pitcher();

            thePitcher.insert_date = DateTime.Now;
            thePitcher.pitcher_name = teamName;
            thePitcher.pitcher_era = ERA;
           

            int res = 0;
            int id_pitcher = 0;

            using (StatsEntities context = new StatsEntities())

            {

                var L2EQuery = context.mlb_pitcher.Where(t => t.pitcher_name == thePitcher.pitcher_name);


                var entityFound = L2EQuery.FirstOrDefault<mlb_pitcher>();

                if (entityFound == null)
                {
                    try
                    {

                        context.mlb_pitcher.Add(thePitcher);
                       
                        res = context.SaveChanges();
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Error" + ex.Message);


                    }

                    Console.WriteLine(res);

                }
                else
                {
                    id_pitcher = entityFound.id_pitcher;
                }

            }

            return id_pitcher;
        }

        public int updateTeam(mlb_team entityFound, mlb_team theEntity, StatsEntities context)
        {
            int res = 0;

            try
            {
                if (entityFound.L10 != theEntity.L10)
                {
                    entityFound.L10 = theEntity.L10;
                    entityFound.last_update_date = DateTime.Now;

                }

                if (entityFound.actualPosition != theEntity.actualPosition)
                {
                    entityFound.actualPosition = theEntity.actualPosition;
                    entityFound.last_update_date = DateTime.Now;

                }

                if (entityFound.league != theEntity.league)
                {
                    entityFound.league = theEntity.league;
                    entityFound.last_update_date = DateTime.Now;

                }

                if (entityFound.division != theEntity.division)
                {
                    entityFound.division = theEntity.division;
                    entityFound.last_update_date = DateTime.Now;

                }

                if (entityFound.win != theEntity.win)
                {
                    entityFound.win = theEntity.win;
                    entityFound.last_update_date = DateTime.Now;

                }

                if (entityFound.lost != theEntity.lost)
                {
                    entityFound.lost = theEntity.lost;
                    entityFound.last_update_date = DateTime.Now;

                }




                //context.mlb_game.

            }
            catch (Exception ex)

            {
                Console.WriteLine("Error" + ex.Message);


            }


            using (context = new StatsEntities())

            {


                context.Entry(entityFound).State = System.Data.Entity.EntityState.Modified;
                res = context.SaveChanges();

                Console.WriteLine(res);

            }

            return entityFound.id_team;
        }

        public int updatePitcher(mlb_pitcher pitcherFound, mlb_pitcher thePitcher, StatsEntities context)
        {
            int res = 0; 

            try
            {
                if (pitcherFound.pitcher_era != thePitcher.pitcher_era)
                {
                    pitcherFound.pitcher_era = thePitcher.pitcher_era;
                    pitcherFound.update_date = DateTime.Now;
                   
                }

               

            }
            catch (Exception ex)

            {
                Console.WriteLine("Error" + ex.Message);


            }


            using (context = new StatsEntities())

            {
               

                context.Entry(pitcherFound).State = System.Data.Entity.EntityState.Modified;
                res = context.SaveChanges();

                Console.WriteLine(res);

            }

            return pitcherFound.id_pitcher;
        }

        public int updatetGame( mlb_game gameFound, mlb_game theGame,  StatsEntities context)
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

                if (gameFound.game_team_home_win != theGame.game_team_home_win)
                {
                    gameFound.game_team_home_win = theGame.game_team_home_win;
                    countChanges++;
                }

                if (gameFound.game_team_home_lost != theGame.game_team_home_lost)
                {
                    gameFound.game_team_home_lost = theGame.game_team_home_lost;
                    countChanges++;
                }


                if (gameFound.game_team_away_win != theGame.game_team_away_win)
                {
                    gameFound.game_team_away_win = theGame.game_team_away_win;
                    countChanges++;
                }

                if (gameFound.game_team_away_lost != theGame.game_team_away_lost)
                {
                    gameFound.game_team_away_lost = theGame.game_team_away_lost;
                    countChanges++;
                }

             



                if (countChanges>0)
                { 
                gameFound.last_update_date = DateTime.Now;
                 insertGameHistory(theGame, gameFound.id_game);

                }
                //context.mlb_game.
                  gameFound.updated = true;
                gameFound.last_version = true;
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error" + ex.Message);


            }

           
            using (context = new StatsEntities())

            {
                


                context.Entry(gameFound).State = System.Data.Entity.EntityState.Modified;
                gameFound.last_version = true;
                res = context.SaveChanges();

                Console.WriteLine(res);

            }

            return gameFound.id_game;
        }

        public void queryExists()
        {

        }

        public object ExeStoredProcedure(string storedProcedureName)
        {
            Dbconnection theConnection = new Dbconnection();

            return theConnection.ExeStoredProcedure(storedProcedureName);
        }

        public DataTable ExeSPWithResults(string storedProcedureName, IDictionary<string, string> parametersDictionary)
        {
            Dbconnection theConnection = new Dbconnection();

            return theConnection.ExeSPWithResults(storedProcedureName, parametersDictionary);
        }

        public DataTable ExeSPWithResults(string storedProcedureName)
        {
            Dbconnection theConnection = new Dbconnection();

            return theConnection.ExeSPWithResults(storedProcedureName);
        }

        public void upsertPitcher(mlb_pitcher theEntity)
        {
            int res = 0;

            using (StatsEntities context = new StatsEntities())

            {

              

                try
                {

                    //If game and date is same, and pitchers change, need to do update.
                    //if GAME AND DATE IS SAME, 
                    //if game date same, 
                    //var L2EQuery = context.mlb_game.Where(g => g.game_date == theGame.game_date && g.game_name_pitcher_away == theGame.game_name_pitcher_away && g.game_name_pitcher_home == theGame.game_name_pitcher_home && g.game_pitcher_away_ERA == theGame.game_pitcher_away_ERA && g.game_pitcher_home_ERA == theGame.game_pitcher_home_ERA );

                    var L2EQuery = context.mlb_pitcher.Where(p => p.pitcher_name == theEntity.pitcher_name);


                    var entityFound = L2EQuery.FirstOrDefault<mlb_pitcher>();

                    if (entityFound == null)
                    {


                        this.insertPitcher(theEntity.pitcher_name, float.Parse(theEntity.pitcher_era.ToString()));




                    }
                    else

                    {
                        updatePitcher(entityFound, theEntity, context);

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

      

        public void upserTeam(mlb_team theEntity)
        {
            int res = 0;
            
            using (StatsEntities context = new StatsEntities())

            {

                try
                {

                
                    int longTeamName = theEntity.team_name.Length;

                    var L2EQuery = context.mlb_team.Where(e => e.team_name.Substring(0,longTeamName-3) == theEntity.team_name.Substring(0,longTeamName-3));

                    var entityFound = L2EQuery.FirstOrDefault<mlb_team>();

                    if (entityFound == null)
                    {

                     this.insertTeam(theEntity.team_name);
                       
                    }
                    else

                    {
                       
                        updateTeam(entityFound, theEntity, context);

                        insertTeamHistory(entityFound);


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

        public void outDateGames()
        {

            using (StatsEntities context = new StatsEntities())

            {

                context.mlb_game.Where(g => g.updated == true).ToList().ForEach(g =>
                {
                    g.updated = false;
                });
                context.SaveChanges();
            }
               
             
        }
        public void upsertGame(mlb_game theGame)
        {
            
            theGame.last_version = true;
            int res = 0;

            using (StatsEntities context = new StatsEntities())

            {

              

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
                        gameFound.last_version = true;
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

        //public object ExeStoredProcedure(string storedProcedureName)
        //{
        //   theData = MLBData.
        //}

        public void selectTeam()
        {
            using (StatsEntities context = new StatsEntities())
            {

                mlb_game theMlbGame = context.mlb_game.FirstOrDefault(r => r.id_game == 1);

                Console.WriteLine(theMlbGame.game_date);
                Console.Read();


            }
        }

       
        public void insertNewTeam()
        {
            using (StatsEntities context = new StatsEntities())
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
            using (StatsEntities context = new StatsEntities())
            {
                mlb_game theMlbGame = context.mlb_game.FirstOrDefault(r => r.id_game == 1);
                context.mlb_game.Remove(theMlbGame);


                context.SaveChanges();

               

            }
        }


        }


    }

