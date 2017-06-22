IF OBJECT_ID ('dbo.SetupConfiguration') IS NOT NULL
	DROP TABLE dbo.SetupConfiguration
GO

CREATE TABLE dbo.SetupConfiguration
(
		SetupConfigurationId	INT				IDENTITY(1, 1)	NOT NULL
	,	ApplicationId			INT								NOT NULL
	,   EntityName				VARCHAR (100)					NOT NULL 	
	,	ConnectionKeyName		VARCHAR (100)					NOT NULL
);

