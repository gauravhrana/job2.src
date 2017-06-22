IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TransactionEvent')
	AND		name	= N'UQ_TransactionEvent_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TransactionEvent_ApplicationId_Name'
	ALTER	TABLE dbo.TransactionEvent
	DROP	CONSTRAINT	UQ_TransactionEvent_ApplicationId_Name
END
GO

ALTER TABLE dbo.TransactionEvent
ADD CONSTRAINT UQ_TransactionEvent_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
