IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TrainStation')
	AND		name	= N'UQ_TrainStation_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TrainStation_ApplicationId_Name'
	ALTER	TABLE dbo.TrainStation
	DROP	CONSTRAINT	UQ_TrainStation_ApplicationId_Name
END
GO

ALTER TABLE dbo.TrainStation
ADD CONSTRAINT UQ_TrainStation_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
