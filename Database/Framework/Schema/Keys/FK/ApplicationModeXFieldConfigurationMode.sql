
ALTER TABLE dbo.ApplicationModeXFieldConfigurationMode
	ADD CONSTRAINT FK_ApplicationModeXFieldConfigurationMode_FieldConfigurationMode FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId
	)
GO


ALTER TABLE dbo.ApplicationModeXFieldConfigurationMode
	ADD CONSTRAINT FK_ApplicationModeXFieldConfigurationMode_ApplicationMode FOREIGN KEY
	(
		ApplicationModeId
	)
	REFERENCES dbo.ApplicationMode
	(
		ApplicationModeId
	)
GO