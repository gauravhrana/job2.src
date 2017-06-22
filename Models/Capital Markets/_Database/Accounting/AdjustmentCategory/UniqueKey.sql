IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AdjustmentCategory')
	AND		name	= N'UQ_AdjustmentCategory_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AdjustmentCategory_ApplicationId_Name'
	ALTER	TABLE dbo.AdjustmentCategory
	DROP	CONSTRAINT	UQ_AdjustmentCategory_ApplicationId_Name
END
GO

ALTER TABLE dbo.AdjustmentCategory
ADD CONSTRAINT UQ_AdjustmentCategory_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
