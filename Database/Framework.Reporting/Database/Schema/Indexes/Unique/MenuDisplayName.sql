IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.MenuDisplayName')
	AND		name	= N'UQ_MenuDisplayName_ApplicationId_MenuId_LanguageId_Value'
)
BEGIN
	PRINT	'Dropping UQ_MenuDisplayName_ApplicationId_MenuId_LanguageId_Value'
	ALTER TABLE dbo.MenuDisplayName
		DROP CONSTRAINT	UQ_MenuDisplayName_ApplicationId_MenuId_LanguageId_Value
END
GO

ALTER TABLE dbo.MenuDisplayName
	ADD CONSTRAINT UQ_MenuDisplayName_ApplicationId_MenuId_LanguageId_Value UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	MenuId
		,	LanguageId
		,	Value
	)
GO