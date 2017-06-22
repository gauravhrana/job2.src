IF OBJECT_ID ('dbo.DatabaseChangeLog') IS NOT NULL
	DROP TABLE dbo.DatabaseChangeLog
GO

CREATE TABLE dbo.DatabaseChangeLog
(
		Id				INT				IDENTITY(1,1)	NOT NULL
	,	DataBaseName	VARCHAR(255)					NULL
	,	SchemaName		VARCHAR(255)					NULL
	,	ObjectName		VARCHAR(255)					NULL
	,	ObjectType		VARCHAR(255)					NULL
	,	EventType		VARCHAR(255)					NULL
	,	RecordDate		DATETIME						NOT NULL
	,	SystemUser		VARCHAR(100)					NULL
	,	CurrentUser		VARCHAR(100)					NULL
	,	OriginalUser	VARCHAR(100)					NULL
	,	EventData		XML
	,	HostName		VARCHAR(100)					NULL	
	,	CommandText		NVARCHAR(MAX)	
) 
