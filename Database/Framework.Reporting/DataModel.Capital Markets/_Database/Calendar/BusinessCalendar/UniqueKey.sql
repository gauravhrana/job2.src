IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].BusinessCalender')
	AND		name	= N'UQ_BusinessCalender_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_BusinessCalender_ApplicationId_Name'
	ALTER	TABLE dbo.BusinessCalender
	DROP	CONSTRAINT	UQ_BusinessCalender_ApplicationId_Name
END
GO

ALTER TABLE dbo.BusinessCalender
ADD CONSTRAINT UQ_BusinessCalender_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
