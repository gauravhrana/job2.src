IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TWRBatchProcessing')
	AND		name	= N'UQ_TWRBatchProcessing_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TWRBatchProcessing_ApplicationId_Name'
	ALTER	TABLE dbo.TWRBatchProcessing
	DROP	CONSTRAINT	UQ_TWRBatchProcessing_ApplicationId_Name
END
GO

ALTER TABLE dbo.TWRBatchProcessing
ADD CONSTRAINT UQ_TWRBatchProcessing_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
