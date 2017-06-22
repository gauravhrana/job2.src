IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].InvestingFeeder')
	AND		name	= N'UQ_InvestingFeeder_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_InvestingFeeder_ApplicationId_Name'
	ALTER	TABLE dbo.InvestingFeeder
	DROP	CONSTRAINT	UQ_InvestingFeeder_ApplicationId_Name
END
GO

ALTER TABLE dbo.InvestingFeeder
ADD CONSTRAINT UQ_InvestingFeeder_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
