IF OBJECT_ID ('dbo.SubscriberApplicationRole') IS NOT NULL
	DROP TABLE dbo.SubscriberApplicationRole
GO

CREATE TABLE dbo.SubscriberApplicationRole 
(
		SubscriberApplicationRoleId		INT				NOT NULL 	
	,	Name							VARCHAR (50)	NOT NULL	
	,	Description						VARCHAR (50)	NOT NULL	
	,	SortOrder						INT				NOT NULL
);

