IF OBJECT_ID ('dbo.ConnectionStringXApplication') IS NOT NULL
	DROP TABLE dbo.ConnectionStringXApplication
GO

CREATE TABLE dbo.ConnectionStringXApplication
(		
		ConnectionStringXApplicationId	INT	IDENTITY(1, 1)		NOT NULL
	,	ConnectionStringId				INT						NOT NULL
	,	ApplicationId					INT						NOT NULL
) 

GO


