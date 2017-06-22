IF OBJECT_ID ('dbo.ApplicationModeXFieldConfigurationMode') IS NOT NULL
	DROP TABLE dbo.ApplicationModeXFieldConfigurationMode
GO


CREATE TABLE ApplicationModeXFieldConfigurationMode
    (
    ApplicationModeXFieldConfigurationModeId	INT NOT NULL,
    ApplicationModeId 							INT NOT NULL,
    FieldConfigurationModeId 					INT NOT NULL,
    ApplicationId 								INT NOT NULL
    )
