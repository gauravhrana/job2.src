IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Meal')
	AND		name	= N'UQ_Meal_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Meal_ApplicationId_Name'
	ALTER	TABLE dbo.Meal
	DROP	CONSTRAINT	UQ_Meal_ApplicationId_Name
END
GO

ALTER TABLE dbo.Meal
ADD CONSTRAINT UQ_Meal_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
