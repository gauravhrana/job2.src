IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AllocationGroup')
	AND		name	= N'UQ_AllocationGroup_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AllocationGroup_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.AllocationGroup
	DROP	CONSTRAINT	UQ_AllocationGroup_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.AllocationGroup
ADD CONSTRAINT UQ_AllocationGroup_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
