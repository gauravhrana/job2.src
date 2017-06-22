IF OBJECT_ID ('dbo.UserLogin') IS NOT NULL
	DROP TABLE dbo.UserLogin
GO

CREATE TABLE dbo.UserLogin
(
		UserLoginId			INT				IDENTITY(1,1)	NOT NULL	
	,	ApplicationId		INT								NOT NULL
	,	UserName			VARCHAR (50)					NOT NULL	
	,	RecordDate			DECIMAL(15,0)					NOT NULL	
	,	UserLoginStatusId	INT								NOT NULL
)
GO
