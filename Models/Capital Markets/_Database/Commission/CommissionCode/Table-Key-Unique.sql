IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CommissionCode')
	AND		name	= N'UQ_CommissionCode_ApplicationId_BrokerId'
)
BEGIN
	PRINT	'Dropping UQ_CommissionCode_ApplicationId_BrokerId'
	ALTER	TABLE dbo.CommissionCode
	DROP	CONSTRAINT	UQ_CommissionCode_ApplicationId_BrokerId
END
GO

ALTER TABLE dbo.CommissionCode
ADD CONSTRAINT UQ_CommissionCode_ApplicationId_BrokerId UNIQUE NONCLUSTERED
(
	ApplicationId, BrokerId
)
GO
