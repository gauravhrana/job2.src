	IF OBJECT_ID ('dbo.ApplicationAttribute') IS NOT NULL
	DROP TABLE dbo.ApplicationAttribute
GO

CREATE TABLE dbo.ApplicationAttribute
	(
	ApplicationId           INT NOT NULL,
	RenderApplicationFilter INT NOT NULL	
	)
GO
