ALTER TABLE dbo.ApplicationOperation
	ADD CONSTRAINT FK_ApplicationOperationId_Application FOREIGN KEY
	(
		ApplicationId
	)
	REFERENCES dbo.Application
	(
		ApplicationId 
	)
GO