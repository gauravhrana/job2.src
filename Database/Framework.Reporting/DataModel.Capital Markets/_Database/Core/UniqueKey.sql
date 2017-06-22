IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TransactionType')
	AND		name	= N'UQ_TransactionType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TransactionType_ApplicationId_Name'
	ALTER	TABLE dbo.TransactionType
	DROP	CONSTRAINT	UQ_TransactionType_ApplicationId_Name
END
GO

ALTER TABLE dbo.TransactionType
ADD CONSTRAINT UQ_TransactionType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
