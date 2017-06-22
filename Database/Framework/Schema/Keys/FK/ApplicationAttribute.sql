ALTER TABLE dbo.ApplicationAttribute
	ADD CONSTRAINT FK_ApplicationAttributeId_Application FOREIGN KEY
	(
		ApplicationId
	)
	REFERENCES dbo.Application
	(
		ApplicationId 
	)
GO