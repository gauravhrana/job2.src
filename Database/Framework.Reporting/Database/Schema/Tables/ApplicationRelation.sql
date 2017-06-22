IF OBJECT_ID ('dbo.ApplicationRelation') IS NOT NULL
	DROP TABLE dbo.ApplicationRelation
GO

CREATE TABLE dbo.ApplicationRelation
(
		ApplicationRelationId				INT		NOT NULL
	,   PublisherApplicationId				INT		NOT NULL 	
	,	SubscriberApplicationId				INT		NOT NULL		
	,	SystemEntityTypeId					INT		NOT NULL
	,	SubscriberApplicationRoleId			INT		NOT NULL
);

