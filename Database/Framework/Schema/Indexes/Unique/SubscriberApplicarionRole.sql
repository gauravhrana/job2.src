IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.SubscriberApplicationRole')
	AND		name	= N'UQ_SubscriberApplicationRole_Name'
)
BEGIN
	PRINT	'Dropping UQ_SubscriberApplicationRole_Name'
	ALTER TABLE dbo.SubscriberApplicationRole
		DROP CONSTRAINT	UQ_SubscriberApplicationRole_Name
END
GO

ALTER TABLE dbo.SubscriberApplicationRole
	ADD CONSTRAINT UQ_SubscriberApplicationRole_Name UNIQUE NONCLUSTERED
	(
			Name
	)
GO
