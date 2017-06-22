IF OBJECT_ID ('dbo.UserLoginStatus') IS NOT NULL
	DROP TABLE dbo.UserLoginStatus
GO

CREATE TABLE dbo.UserLoginStatus
(
		UserLoginStatusId		INT				NOT NULL	
	,	ApplicationId			INT				NOT NULL
	,	Name					VARCHAR (50)	NOT NULL	
	,	Description				VARCHAR (50)	NOT NULL	
	,	SortOrder				INT				NOT NULL
)
GO
