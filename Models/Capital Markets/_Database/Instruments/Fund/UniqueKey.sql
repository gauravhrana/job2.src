IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Fund')
	AND		name	= N'UQ_Fund_ApplicationId_ManagementFirmId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Fund_ApplicationId_ManagementFirmId_Name'
	ALTER	TABLE dbo.Fund
	DROP	CONSTRAINT	UQ_Fund_ApplicationId_ManagementFirmId_Name
END
GO

ALTER TABLE dbo.Fund
ADD CONSTRAINT UQ_Fund_ApplicationId_ManagementFirmId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, ManagementFirmId, Name
)
GO
