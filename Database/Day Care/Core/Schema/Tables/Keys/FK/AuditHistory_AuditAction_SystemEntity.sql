ALTER TABLE dbo.AuditHistory
	ADD CONSTRAINT FK_AuditHistory_AuditAction FOREIGN KEY 
	(
		AuditActionId
	) 
	REFERENCES AuditAction
	(
		AuditActionId
	)
	GO
	ALTER TABLE dbo.AuditHistory
	ADD CONSTRAINT FK_AuditHistory_SystemEntityType FOREIGN KEY 
	(
		SystemEntityTypeId
	) 
	REFERENCES SystemEntityType
	(
		SystemEntityTypeId
	)
GO
	
