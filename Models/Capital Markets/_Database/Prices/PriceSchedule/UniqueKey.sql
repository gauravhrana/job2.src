IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PriceSchedule')
	AND		name	= N'UQ_PriceSchedule_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PriceSchedule_ApplicationId_Name'
	ALTER	TABLE dbo.PriceSchedule
	DROP	CONSTRAINT	UQ_PriceSchedule_ApplicationId_Name
END
GO

ALTER TABLE dbo.PriceSchedule
ADD CONSTRAINT UQ_PriceSchedule_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
