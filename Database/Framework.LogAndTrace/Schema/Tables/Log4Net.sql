IF OBJECT_ID ('dbo.Log4Net') IS NOT NULL
	DROP TABLE dbo.Log4Net
GO

CREATE TABLE dbo.Log4Net
(
		Id				INT			IDENTITY(1,1)	NOT NULL
	,	LogUser			VARCHAR(255)				NULL
	,	Date			DATETIME					NOT NULL
	,	StackTrace		VARCHAR(6000)
	,	Thread			VARCHAR(255)				NOT NULL
	,	Level			VARCHAR(50)					NOT NULL	
	,	Logger			VARCHAR(255)				NOT NULL
	,	Message			VARCHAR(4000)				NOT NULL
	,	Computer		VARCHAR(100)
	,	Exception		VARCHAR(2000)				NULL
	,	ApplicationId	VARCHAR(50)					NULL
	,	ConnectionKey	VARCHAR(50)					NULL
) 

