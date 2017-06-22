IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Broker')
	AND		name	= N'UQ_Broker_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Broker_ApplicationId_Name'
	ALTER	TABLE dbo.Broker
	DROP	CONSTRAINT	UQ_Broker_ApplicationId_Name
END
GO

ALTER TABLE dbo.Broker
ADD CONSTRAINT UQ_Broker_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
