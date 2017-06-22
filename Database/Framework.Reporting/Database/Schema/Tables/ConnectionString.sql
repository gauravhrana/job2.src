IF OBJECT_ID ('dbo.ConnectionString') IS NOT NULL
	DROP TABLE dbo.ConnectionString
GO

CREATE TABLE dbo.ConnectionString 
(
		ConnectionStringId			INT				NOT NULL 	
	,	Name						VARCHAR (100)	NOT NULL	
	,	Description					VARCHAR (100)	NOT NULL	
	,	DataSource					VARCHAR (100)	NOT NULL	
	,	InitialCatalog				VARCHAR (100)	NOT NULL	
	,	UserName					VARCHAR (100)	NOT NULL
	,	Password					VARCHAR (100)	NOT NULL
	,	ProviderName				VARCHAR (100)	NOT NULL
);