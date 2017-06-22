IF OBJECT_Id ('dbo.MealType') IS NOT NULL
   DROP TABLE dbo.MealType
GO

CREATE TABLE dbo.MealType 
(
		 MealTypeId		 INT          NOT NULL
	,	 ApplicationId   INT          NOT NULL
	,	 Name			 VARCHAR (50) NOT NULL
	,	 Description     VARCHAR (500) NOT NULL
	,	 SortOrder		 INT          NOT NULL
)
GO

