IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityXSettlementDay')
	AND		name	= N'UQ_SecurityXSettlementDay_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SecurityXSettlementDay_ApplicationId_Name'
	ALTER	TABLE dbo.SecurityXSettlementDay
	DROP	CONSTRAINT	UQ_SecurityXSettlementDay_ApplicationId_Name
END
GO

ALTER TABLE dbo.SecurityXSettlementDay
ADD CONSTRAINT UQ_SecurityXSettlementDay_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
