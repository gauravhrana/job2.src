IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.Menu')
	AND		name	= N'UQ_Menu_ApplicationId_Name_ParentMenuId'
)
BEGIN
	PRINT	'Dropping UQ_Menu_ApplicationId_Name_ParentMenuId'
	ALTER TABLE dbo.Menu
		DROP CONSTRAINT	UQ_Menu_ApplicationId_Name_ParentMenuId
END
GO

ALTER TABLE dbo.Menu
	ADD CONSTRAINT UQ_Menu_ApplicationId_Name_ParentMenuId UNIQUE NONCLUSTERED
	(
			ApplicationId	
		,	Name			
		,	ParentMenuId	
	)
GO
