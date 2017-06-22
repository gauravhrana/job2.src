IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountingCalender')
	AND		name	= N'UQ_AccountingCalender_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountingCalender_ApplicationId_Name'
	ALTER	TABLE dbo.AccountingCalender
	DROP	CONSTRAINT	UQ_AccountingCalender_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountingCalender
ADD CONSTRAINT UQ_AccountingCalender_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
