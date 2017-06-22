IF OBJECT_ID ('dbo.SystemEntityType') IS NOT NULL
	DROP TABLE dbo.SystemEntityType
GO

CREATE TABLE dbo.SystemEntityType
(
	SystemEntityTypeId		INT				NOT NULL,
	EntityName				VARCHAR (100)	NOT NULL,
	EntityDescription       VARCHAR (250)	NOT NULL,
	PrimaryDatabase			VARCHAR (50)	NOT NULL,
	CreatedDate			    DateTime		NOT NULL,
	NextValue				INT				NOT NULL,
	IncreaseBy				INT				DEFAULT '1',
)
GO
