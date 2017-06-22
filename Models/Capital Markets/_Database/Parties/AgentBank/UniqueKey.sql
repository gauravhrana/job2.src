IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AgentBank')
	AND		name	= N'UQ_AgentBank_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AgentBank_ApplicationId_Name'
	ALTER	TABLE dbo.AgentBank
	DROP	CONSTRAINT	UQ_AgentBank_ApplicationId_Name
END
GO

ALTER TABLE dbo.AgentBank
ADD CONSTRAINT UQ_AgentBank_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
