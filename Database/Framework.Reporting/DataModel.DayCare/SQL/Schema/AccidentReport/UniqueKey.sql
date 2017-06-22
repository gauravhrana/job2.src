IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccidentReport')
	AND		name	= N'UQ_AccidentReport_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccidentReport_ApplicationId_Name'
	ALTER	TABLE dbo.AccidentReport
	DROP	CONSTRAINT	UQ_AccidentReport_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccidentReport
ADD CONSTRAINT UQ_AccidentReport_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
