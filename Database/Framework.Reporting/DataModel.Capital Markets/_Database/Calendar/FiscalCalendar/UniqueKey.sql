IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].FiscalCalender')
	AND		name	= N'UQ_FiscalCalender_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_FiscalCalender_ApplicationId_Name'
	ALTER	TABLE dbo.FiscalCalender
	DROP	CONSTRAINT	UQ_FiscalCalender_ApplicationId_Name
END
GO

ALTER TABLE dbo.FiscalCalender
ADD CONSTRAINT UQ_FiscalCalender_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
