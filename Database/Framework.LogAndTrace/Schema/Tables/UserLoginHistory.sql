IF OBJECT_ID ('dbo.UserLoginHistory') IS NOT NULL
	DROP TABLE dbo.UserLoginHistory
GO

CREATE TABLE dbo.UserLoginHistory
(
		UserLoginHistoryId		INT			IDENTITY(1,1)	NOT NULL	
	,	ApplicationId			INT				NOT NULL
	,	UserId					INT 	NOT NULL	
	,	UserName				VARCHAR (50)	NOT NULL	
	,	URL						VARCHAR (200)				NOT NULL
	,	ServerName						VARCHAR (50)				NOT NULL
	,	DateVisited				DateTime		NOT NULL
)
GO
