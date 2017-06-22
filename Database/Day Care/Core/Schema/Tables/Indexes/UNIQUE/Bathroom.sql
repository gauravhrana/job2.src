IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Bathroom]')
	AND		name	= N'UQ_Bathroom_ApplicationId_StudentId_TimeIn'
)
BEGIN
	PRINT	'Dropping UQ_Bathroom_ApplicationId_StudentId_TimeIn'
	ALTER	TABLE dbo.Bathroom
	DROP	CONSTRAINT	UQ_Bathroom_ApplicationId_StudentId_TimeIn
END
GO

ALTER TABLE dbo.Bathroom
ADD CONSTRAINT UQ_Bathroom_ApplicationId_StudentId_TimeIn UNIQUE NONCLUSTERED
(
		ApplicationId
	,	StudentId
	,	TimeIn	
)
GO
