IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ManagementFirm')
	AND		name	= N'UQ_ManagementFirm_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ManagementFirm_ApplicationId_Name'
	ALTER	TABLE dbo.ManagementFirm
	DROP	CONSTRAINT	UQ_ManagementFirm_ApplicationId_Name
END
GO

ALTER TABLE dbo.ManagementFirm
ADD CONSTRAINT UQ_ManagementFirm_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
