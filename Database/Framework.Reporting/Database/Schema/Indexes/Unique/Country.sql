IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.Country')
	AND		name	= N'UQ_Country_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_Country_Name_ApplicationId'
	ALTER TABLE dbo.Country
		DROP CONSTRAINT	UQ_Country_Name_ApplicationId
END
GO

ALTER TABLE dbo.Country
	ADD CONSTRAINT UQ_Country_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
