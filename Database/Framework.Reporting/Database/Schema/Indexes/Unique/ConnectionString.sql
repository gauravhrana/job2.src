IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ConnectionString')
	AND		name	= N'UQ_ConnectionString_Name'
)
BEGIN
	PRINT	'Dropping UQ_ConnectionString_Name'
	ALTER TABLE dbo.ConnectionString
		DROP CONSTRAINT	UQ_ConnectionString_Name
END
GO

ALTER TABLE dbo.ConnectionString
	ADD CONSTRAINT UQ_ConnectionString_Name UNIQUE NONCLUSTERED
	(
			Name
	)
GO
