IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[AccidentReport]')
	AND		name	= N'UQ_AccidentReport_ApplicationId_StudentId_AccidentPlaceId_Date_TeacherId'
)
BEGIN
	PRINT	'Dropping UQ_AccidentReport_ApplicationId_StudentId_AccidentPlaceId_Date_TeacherId'
	ALTER	TABLE dbo.AccidentReport
	DROP	CONSTRAINT	UQ_AccidentReport_ApplicationId_StudentId_AccidentPlaceId_Date_TeacherId
	
END
GO

ALTER TABLE dbo.AccidentReport
ADD CONSTRAINT UQ_AccidentReport_ApplicationId_StudentId_AccidentPlaceId_Date_TeacherId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	StudentId
	,	AccidentPlaceId	
	,	Date
	,	TeacherId
)
GO
