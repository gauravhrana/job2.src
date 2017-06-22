IF OBJECT_Id ('dbo.AboutPages') IS NOT NULL
	DROP TABLE dbo.AboutPages
GO
	
CREATE TABLE dbo.AboutPages 
(
		AboutPagesId	INT			  NOT NULL	
	,	ApplicationId   INT           NOT NULL
	,	Description     VARCHAR (500) NOT NULL	
	,	Developer       VARCHAR (100) NOT NULL
	,	JIRAId			VARCHAR (100) NOT NULL
	,	Feature			VARCHAR (100) NOT NULL
	,	PrimaryEntity	VARCHAR (100) NOT NULL
)
GO

