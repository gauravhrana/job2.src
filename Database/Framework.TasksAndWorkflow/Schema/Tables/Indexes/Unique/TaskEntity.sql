IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.TaskEntity')
	AND		name	= N'UQ_TaskEntity_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaskEntityOperation_Name'
	ALTER TABLE dbo.TaskEntity
		DROP CONSTRAINT	UQ_TaskEntity_ApplicationId_Name
END
GO

ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT UQ_TaskEntity_ApplicationId_Name UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	Name
	)
GO
