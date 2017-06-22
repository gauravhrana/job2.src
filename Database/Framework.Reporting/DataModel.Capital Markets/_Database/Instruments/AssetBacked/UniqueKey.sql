IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AssetBacked')
	AND		name	= N'UQ_AssetBacked_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AssetBacked_ApplicationId_Name'
	ALTER	TABLE dbo.AssetBacked
	DROP	CONSTRAINT	UQ_AssetBacked_ApplicationId_Name
END
GO

ALTER TABLE dbo.AssetBacked
ADD CONSTRAINT UQ_AssetBacked_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
