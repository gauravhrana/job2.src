IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].BathRoom')
	AND		name	= N'UQ_BathRoom_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_BathRoom_ApplicationId_Name'
	ALTER	TABLE dbo.BathRoom
	DROP	CONSTRAINT	UQ_BathRoom_ApplicationId_Name
END
GO

ALTER TABLE dbo.BathRoom
ADD CONSTRAINT UQ_BathRoom_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
