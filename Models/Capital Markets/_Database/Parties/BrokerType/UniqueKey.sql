IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].BrokerType')
	AND		name	= N'UQ_BrokerType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_BrokerType_ApplicationId_Name'
	ALTER	TABLE dbo.BrokerType
	DROP	CONSTRAINT	UQ_BrokerType_ApplicationId_Name
END
GO

ALTER TABLE dbo.BrokerType
ADD CONSTRAINT UQ_BrokerType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
