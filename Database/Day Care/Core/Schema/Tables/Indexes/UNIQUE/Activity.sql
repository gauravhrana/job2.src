IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Activity]')
	AND		name	= N'UQ_Activity_ApplicationId_StudentId_ActivityTypeId_ActivitySubTypeId'
)
BEGIN
	PRINT	'Dropping UQ_Activity_ApplicationId_StudentId_ActivityTypeId_ActivitySubTypeId'
	ALTER	TABLE dbo.Activity
	DROP	CONSTRAINT	UQ_Activity_ApplicationId_StudentId_ActivityTypeId_ActivitySubTypeId
END
GO

ALTER TABLE dbo.Activity
ADD CONSTRAINT UQ_Activity_ApplicationId_StudentId_ActivityTypeId_ActivitySubTypeId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	StudentId
	,	ActivityTypeId
	,	ActivitySubTypeId	
)
GO















