--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_TypeOfIssue_Description'
--)

ALTER TABLE dbo.TypeOfIssue
	ADD CONSTRAINT DF_TypeOfIssue_Description		DEFAULT '' 		FOR Description
GO


--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_TypeOfIssue_SortOrder'
--)

ALTER TABLE dbo.TypeOfIssue
	ADD CONSTRAINT DF_TypeOfIssue_SortOrder		DEFAULT 1000		FOR SortOrder
GO
