IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CommissionSplit')
	AND		name	= N'UQ_CommissionSplit_ApplicationId_CommissionCodeId'
)
BEGIN
	PRINT	'Dropping UQ_CommissionSplit_ApplicationId_CommissionCodeId'
	ALTER	TABLE dbo.CommissionSplit
	DROP	CONSTRAINT	UQ_CommissionSplit_ApplicationId_CommissionCodeId
END
GO

ALTER TABLE dbo.CommissionSplit
ADD CONSTRAINT UQ_CommissionSplit_ApplicationId_CommissionCodeId UNIQUE NONCLUSTERED
(
	ApplicationId, CommissionCodeId
)
GO
