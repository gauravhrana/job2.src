IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.MenuCategory')
	AND		name	= N'UQ_MenuCategory_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_MenuCategory_Name_ApplicationId'
	ALTER TABLE dbo.MenuCategory
		DROP CONSTRAINT	UQ_MenuCategory_Name_ApplicationId
END
GO

ALTER TABLE dbo.MenuCategory
	ADD CONSTRAINT UQ_MenuCategory_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
