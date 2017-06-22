IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NeedItem]')
	AND		name	= N'UQ_NeedItem_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_NeedItem_ApplicationId_Name'
	ALTER	TABLE dbo.NeedItem
	DROP	CONSTRAINT	UQ_NeedItem_ApplicationId_Name
END
GO

ALTER TABLE dbo.NeedItem
ADD CONSTRAINT UQ_NeedItem_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
