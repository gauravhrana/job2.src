IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SubClass')
	AND		name	= N'UQ_SubClass_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SubClass_ApplicationId_Name'
	ALTER	TABLE dbo.SubClass
	DROP	CONSTRAINT	UQ_SubClass_ApplicationId_Name
END
GO

ALTER TABLE dbo.SubClass
ADD CONSTRAINT UQ_SubClass_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
