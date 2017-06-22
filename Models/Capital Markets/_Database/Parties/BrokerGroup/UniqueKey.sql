IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].BrokerGroup')
	AND		name	= N'UQ_BrokerGroup_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_BrokerGroup_ApplicationId_Name'
	ALTER	TABLE dbo.BrokerGroup
	DROP	CONSTRAINT	UQ_BrokerGroup_ApplicationId_Name
END
GO

ALTER TABLE dbo.BrokerGroup
ADD CONSTRAINT UQ_BrokerGroup_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
