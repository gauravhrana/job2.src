IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Series')
	AND		name	= N'UQ_Series_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Series_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.Series
	DROP	CONSTRAINT	UQ_Series_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.Series
ADD CONSTRAINT UQ_Series_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
