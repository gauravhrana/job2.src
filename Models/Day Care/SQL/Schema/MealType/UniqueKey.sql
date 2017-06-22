IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MealType')
	AND		name	= N'UQ_MealType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_MealType_ApplicationId_Name'
	ALTER	TABLE dbo.MealType
	DROP	CONSTRAINT	UQ_MealType_ApplicationId_Name
END
GO

ALTER TABLE dbo.MealType
ADD CONSTRAINT UQ_MealType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
