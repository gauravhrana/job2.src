ALTER TABLE dbo.AuditHistory
	ADD CONSTRAINT FK_AuditHistoryId_AuditAction FOREIGN KEY
	(
		AuditActionId
	)
	REFERENCES dbo.AuditAction
	(
		AuditActionId 
	)
GO