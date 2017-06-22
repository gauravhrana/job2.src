IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Calendar')
	AND		name	= N'UQ_Calendar_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Calendar_ApplicationId_Name'
	ALTER	TABLE dbo.Calendar
	DROP	CONSTRAINT	UQ_Calendar_ApplicationId_Name
END
GO

ALTER TABLE dbo.Calendar
ADD CONSTRAINT UQ_Calendar_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
