IF OBJECT_ID ('dbo.SystemEntityXSystemEntityCategory') IS NOT NULL
	DROP TABLE dbo.SystemEntityXSystemEntityCategory
GO
CREATE TABLE dbo.SystemEntityXSystemEntityCategory
(	SystemEntityXSystemEntityCategoryId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	SystemEntityId			INT NOT NULL,
	SystemEntityCategoryId			INT NOT NULL
) 

GO


