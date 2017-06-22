﻿IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SubscriberApplicationRole_Description'
)

ALTER TABLE dbo.SubscriberApplicationRole
	ADD CONSTRAINT DF_SubscriberApplicationRole_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SubscriberApplicationRole_SortOrder'
)

ALTER TABLE dbo.SubscriberApplicationRole
	ADD CONSTRAINT DF_SubscriberApplicationRole_SortOrder		DEFAULT 1000		FOR SortOrder
GO