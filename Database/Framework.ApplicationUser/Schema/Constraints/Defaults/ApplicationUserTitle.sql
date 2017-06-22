--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_ApplicationUserTitle_Description'
--)

ALTER TABLE dbo.ApplicationUserTitle
	ADD CONSTRAINT DF_ApplicationUserTitle_Description		DEFAULT '' 		FOR Description
GO


--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_ApplicationUserTitle_SortOrder'
--)

ALTER TABLE dbo.ApplicationUserTitle
	ADD CONSTRAINT DF_ApplicationUserTitle_SortOrder		DEFAULT 1000		FOR SortOrder
GO
