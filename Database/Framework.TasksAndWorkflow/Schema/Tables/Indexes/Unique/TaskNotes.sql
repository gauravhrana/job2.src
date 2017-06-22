IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TaskNotes]')
	AND		name	= N'UQ_TaskNotes_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaskNotes_Name'
	ALTER	TABLE dbo.TaskNotes
	DROP	CONSTRAINT	UQ_TaskNotes_Name
END
GO

ALTER TABLE dbo.TaskNotes
ADD CONSTRAINT UQ_TaskNotes_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
