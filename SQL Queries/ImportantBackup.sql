USE [DonBest]
GO
/****** Object:  User [luisma]    Script Date: 8/31/2017 7:26:04 AM ******/
CREATE USER [luisma] FOR LOGIN [luisma] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [luisma]
GO
/****** Object:  Table [dbo].[mlb_game]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mlb_game](
	[id_game] [int] IDENTITY(1,1) NOT NULL,
	[game_date] [datetime] NULL,
	[game_serie_id] [int] NOT NULL,
	[game_number] [int] NULL,
	[game_id_team_home] [int] NOT NULL,
	[game_id_team_away] [int] NOT NULL,
	[game_name_team_home] [varchar](max) NULL,
	[game_name_team_away] [varchar](max) NOT NULL,
	[game_name_pitcher_home] [varchar](max) NULL,
	[game_name_pitcher_away] [varchar](max) NULL,
	[game_pitcher_home_ERA] [float] NULL,
	[game_pitcher_away_ERA] [float] NULL,
	[updated] [bit] NULL,
	[insert_date] [datetime] NULL,
	[last_update_date] [datetime] NULL,
	[last_version] [bit] NULL,
 CONSTRAINT [PK_mlb_game] PRIMARY KEY CLUSTERED 
(
	[id_game] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mlb_pitcher]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mlb_pitcher](
	[id_pitcher] [int] IDENTITY(1,1) NOT NULL,
	[pitcher_name] [varchar](max) NULL,
	[pitcher_era] [float] NULL,
	[pitcher_last3_era] [float] NULL,
	[insert_date] [datetime] NULL,
	[update_date] [datetime] NULL,
 CONSTRAINT [PK_mlb_pitcher] PRIMARY KEY CLUSTERED 
(
	[id_pitcher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mlb_serie]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mlb_serie](
	[id_serie] [int] IDENTITY(1,1) NOT NULL,
	[mlb_game_id] [int] NOT NULL,
	[mlb_quantity_games] [int] NULL,
 CONSTRAINT [PK_mlb_serie] PRIMARY KEY CLUSTERED 
(
	[id_serie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mlb_team]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mlb_team](
	[id_team] [int] IDENTITY(1,1) NOT NULL,
	[team_name] [varchar](max) NOT NULL,
	[insert_date] [datetime] NULL,
	[L10] [varchar](50) NULL,
	[actualPosition] [int] NULL,
	[league] [varchar](50) NULL,
	[division] [varchar](50) NULL,
	[last_update_date] [datetime] NULL,
	[win] [int] NULL,
	[lost] [int] NULL,
 CONSTRAINT [PK_mlb_team] PRIMARY KEY CLUSTERED 
(
	[id_team] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[view_schedule_mlb]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_schedule_mlb]
AS
SELECT        TOP (1000) id_game, game_date, game_name_team_home, game_name_team_away, game_name_pitcher_home, game_name_pitcher_away, game_pitcher_home_ERA, game_pitcher_away_ERA, updated, 
                         insert_date, last_update_date, game_serie_id, game_number, game_id_team_home, game_id_team_away
FROM            dbo.mlb_game
WHERE        (game_date > '2017-08-30') AND (game_date < '2017-08-31') AND (updated = 1)
ORDER BY game_date, id_game

GO
/****** Object:  StoredProcedure [dbo].[setAllGamesNotUpdated]    Script Date: 8/31/2017 7:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
CREATE PROCEDURE [dbo].[setAllGamesNotUpdated]
    -- Add the parameters for the stored procedure here
   
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    Update [DonBest].[dbo].[mlb_game] 
    set updated=0;

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[4] 2[4] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "mlb_game"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_schedule_mlb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_schedule_mlb'
GO
