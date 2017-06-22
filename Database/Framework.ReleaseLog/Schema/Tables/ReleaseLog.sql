
CREATE TABLE dbo.ReleaseLog
	(
	ReleaseLogId       INT				NOT NULL,
	ApplicationId	   INT				NOT NULL,
	ReleaseLogId        INT				NOT NULL,	
	Name               VARCHAR(50)		NOT NULL,
	VersionNo          VARCHAR(50)		NOT NULL,
	ReleaseDate        DATETIME			NOT NULL,	
	Description        VARCHAR(50)		NOT NULL,
	SortOrder		   INT				NOT NULL
	)
GO

