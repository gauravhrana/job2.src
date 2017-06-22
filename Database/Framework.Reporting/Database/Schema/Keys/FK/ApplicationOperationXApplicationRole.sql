ALTER TABLE dbo.ApplicationOperationXApplicationRole
	ADD CONSTRAINT FK_ApplicationOperationXApplicationRole_Application FOREIGN KEY
	(
		ApplicationId
	)
	REFERENCES Application
	(
		ApplicationId
	)
GO

ALTER TABLE dbo.ApplicationOperationXApplicationRole
	ADD CONSTRAINT FK_ApplicationOperationXApplicationRole_ApplicationOperation FOREIGN KEY
	(
		ApplicationOperationId
	)
	REFERENCES dbo.ApplicationOperation
	(
		ApplicationOperationId
	)
GO


ALTER TABLE dbo.ApplicationOperationXApplicationRole
	ADD CONSTRAINT FK_ApplicationOperationXApplicationRole_ApplicationRole FOREIGN KEY
	(
		ApplicationRoleId
	)
	REFERENCES dbo.ApplicationRole
	(
		ApplicationRoleId
	)
GO

