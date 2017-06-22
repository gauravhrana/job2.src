IF OBJECT_ID ('dbo.PublishXDevelopment') IS NOT NULL
	DROP TABLE dbo.PublishXDevelopment
GO
CREATE TABLE dbo.PublishXDevelopment
(	
	PublishXDevelopmentId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	PublishId			INT NOT NULL,
	DevelopmentId			INT NOT NULL
) 

GO


