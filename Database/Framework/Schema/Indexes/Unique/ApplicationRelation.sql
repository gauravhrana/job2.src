IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationRelation')
	AND		name	= N'UQ_ApplicationRelation_PublisherApplicationId_SubscriberApplicationId_SystemEntityTypeId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationRelation_PublisherApplicationId_SubscriberApplicationId_SystemEntityTypeId'
	ALTER TABLE dbo.ApplicationRelation
		DROP CONSTRAINT	UQ_ApplicationRelation_PublisherApplicationId_SubscriberApplicationId_SystemEntityTypeId
END
GO

ALTER TABLE dbo.ApplicationRelation
	ADD CONSTRAINT UQ_ApplicationRelation_PublisherApplicationId_SubscriberApplicationId_SystemEntityTypeId UNIQUE NONCLUSTERED
	(
			PublisherApplicationId
		,	SubscriberApplicationId
		,	SystemEntityTypeId
	)
GO
