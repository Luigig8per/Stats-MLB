//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace clsModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class mlb_view_games_by_update_date
    {
        public Nullable<System.DateTime> game_date { get; set; }
        public string game_name_team_home { get; set; }
        public string game_name_team_away { get; set; }
        public string game_name_pitcher_home { get; set; }
        public string game_name_pitcher_away { get; set; }
        public Nullable<double> game_pitcher_home_ERA { get; set; }
        public Nullable<double> game_pitcher_away_ERA { get; set; }
        public Nullable<bool> updated { get; set; }
        public Nullable<System.DateTime> insert_date { get; set; }
        public Nullable<System.DateTime> last_update_date { get; set; }
        public Nullable<bool> last_version { get; set; }
        public string game_team_home_L10 { get; set; }
        public Nullable<int> game_team_home_win { get; set; }
        public Nullable<int> game_team_home_lost { get; set; }
        public Nullable<int> game_team_home_position { get; set; }
        public string game_team_away_L10 { get; set; }
        public Nullable<int> game_team_away_win { get; set; }
        public Nullable<int> game_team_away_lost { get; set; }
        public Nullable<int> game_team_away_position { get; set; }
    }
}