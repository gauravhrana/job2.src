ALTER TABLE dbo.ApplicationModeXApplicationEntityFieldLabelMode
	ADD CONSTRAINT FK_ApplicationModeXApplicationEntityFieldLabelMode_Application FOREIGN KEY
	(
		ApplicationId
	)
	REFERENCES Application
	(
		ApplicationId
	)
GO

ALTER TABLE dbo.ApplicationModeXApplicationEntityFieldLabelMode
	ADD CONSTRAINT FK_ApplicationModeXApplicationEntityFieldLabelMode_ApplicationMode FOREIGN KEY
	(
		ApplicationModeId
	)
	REFERENCES dbo.ApplicationMode
	(
		ApplicationModeId
	)
GO


ALTER TABLE dbo.ApplicationModeXApplicationEntityFieldLabelMode
	ADD CONSTRAINT FK_ApplicationModeXApplicationEntityFieldLabelMode_ApplicationEntityFieldLabelMode FOREIGN KEY
	(
		ApplicationEntityFieldLabelModeId
	)
	REFERENCES dbo.ApplicationEntityFieldLabelMode
	(
		ApplicationEntityFieldLabelModeId
	)
GO

