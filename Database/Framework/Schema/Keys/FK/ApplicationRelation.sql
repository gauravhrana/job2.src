ALTER TABLE dbo.ApplicationRelation
	ADD CONSTRAINT FK_ApplicationRelation_SubscribeApplcationRole FOREIGN KEY
	(
		SubscriberApplicationRoleId
	)
	REFERENCES dbo.SubscriberApplicationRole
	(
		SubscriberApplicationRoleId 
	)
GO