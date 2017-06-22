IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MealDetail')
	AND		name	= N'UQ_MealDetail_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_MealDetail_ApplicationId_Name'
	ALTER	TABLE dbo.MealDetail
	DROP	CONSTRAINT	UQ_MealDetail_ApplicationId_Name
END
GO

ALTER TABLE dbo.MealDetail
ADD CONSTRAINT UQ_MealDetail_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
