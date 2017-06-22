IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[FoodType]')
	AND		name	= N'UQ_FoodType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_FoodType_ApplicationId_Name'
	ALTER	TABLE dbo.FoodType
	DROP	CONSTRAINT	UQ_FoodType_ApplicationId_Name
END
GO

ALTER TABLE dbo.FoodType
ADD CONSTRAINT UQ_FoodType_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
