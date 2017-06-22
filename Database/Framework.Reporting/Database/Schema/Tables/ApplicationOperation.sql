IF OBJECT_ID ('dbo.ApplicationOperation') IS NOT NULL
	DROP TABLE dbo.ApplicationOperation
GO

CREATE TABLE dbo.ApplicationOperation 
(
		ApplicationOperationId		INT				NOT NULL
	,   ApplicationId				INT				NOT NULL 	
	,	Name						VARCHAR (50)	NOT NULL	
	,	Description					VARCHAR (50)	NOT NULL	
	,	SortOrder					INT				NOT NULL
	,	SystemEntityTypeId          INT				NOT NULL
	,	OperationValue				VARCHAR(50)		NOT NULL
);

