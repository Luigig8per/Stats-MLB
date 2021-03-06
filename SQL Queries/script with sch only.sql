USE [DonBest]
GO
/****** Object:  Table [dbo].[mlb_game]    Script Date: 8/31/2017 7:49:13 AM ******/
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
/****** Object:  Table [dbo].[mlb_pitcher]    Script Date: 8/31/2017 7:49:13 AM ******/
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
/****** Object:  Table [dbo].[mlb_serie]    Script Date: 8/31/2017 7:49:13 AM ******/
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
/****** Object:  Table [dbo].[mlb_team]    Script Date: 8/31/2017 7:49:13 AM ******/
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
/****** Object:  Table [dbo].[mlb_team_history]    Script Date: 8/31/2017 7:49:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mlb_team_history](
	[id_team_history] [int] NOT NULL,
	[id_team] [int] NOT NULL,
	[insert_date] [datetime] NOT NULL,
	[L10] [nchar](10) NOT NULL,
	[position] [int] NOT NULL,
	[win] [int] NOT NULL,
	[lost] [int] NOT NULL,
 CONSTRAINT [PK_mlb_team_history] PRIMARY KEY CLUSTERED 
(
	[id_team_history] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
