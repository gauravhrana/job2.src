IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].InventoryState')
	AND		name	= N'UQ_InventoryState_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_InventoryState_ApplicationId_Name'
	ALTER	TABLE dbo.InventoryState
	DROP	CONSTRAINT	UQ_InventoryState_ApplicationId_Name
END
GO

ALTER TABLE dbo.InventoryState
ADD CONSTRAINT UQ_InventoryState_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
