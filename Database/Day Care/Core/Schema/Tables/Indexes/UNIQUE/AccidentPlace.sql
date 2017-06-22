IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[AccidentPlace]')
	AND		name	= N'UQ_AccidentPlace_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccidentPlace_ApplicationId_Name'
	ALTER	TABLE dbo.AccidentPlace
	DROP	CONSTRAINT	UQ_AccidentPlace_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccidentPlace
ADD CONSTRAINT UQ_AccidentPlace_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
