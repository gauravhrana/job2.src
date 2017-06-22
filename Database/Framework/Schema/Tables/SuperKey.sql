IF OBJECT_ID ('dbo.SuperKey') IS NOT NULL
	DROP TABLE dbo.SuperKey
GO

CREATE TABLE dbo.SuperKey 
(
		SuperKeyId			INT				IDENTITY(1, 1)	NOT NULL 
	,   ApplicationId		INT								NOT NULL 	
	,	Name				VARCHAR (50)					NOT NULL	
	,	Description			VARCHAR (500)					NOT NULL	
	,	SortOrder			INT								NOT NULL
	,	SystemEntityTypeId	INT								NOT NULL
	,	ExpirationDate		INT								NOT NULL
);

