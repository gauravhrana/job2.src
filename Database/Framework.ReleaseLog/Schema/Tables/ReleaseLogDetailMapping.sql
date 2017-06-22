IF OBJECT_ID ('dbo.ReleaseLogDetailMapping') IS NOT NULL
	DROP TABLE dbo.ReleaseLogDetailMapping
GO
CREATE TABLE dbo.ReleaseLogDetailMapping
(	
	ReleaseLogDetailMappingId				INT NOT NULL,
	ApplicationId							INT NOT NULL,
	ParentReleaseLogDetailId				INT NOT NULL,
	ChildReleaseLogDetailId					INT NOT NULL
) 

GO


