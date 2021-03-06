﻿IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationOperation_Description'
)

ALTER TABLE dbo.ApplicationOperation
	ADD CONSTRAINT DF_ApplicationOperation_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationOperation_SortOrder'
)

ALTER TABLE dbo.ApplicationOperation
	ADD CONSTRAINT DF_ApplicationOperation_SortOrder		DEFAULT 1000		FOR SortOrder
GO
