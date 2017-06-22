IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationOperation')
	AND		name	= N'UQ_ApplicationOperation_Name'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationOperationOperation_Name'
	ALTER TABLE dbo.ApplicationOperation
		DROP CONSTRAINT	UQ_ApplicationOperation_Name
END
GO

ALTER TABLE dbo.ApplicationOperation
	ADD CONSTRAINT UQ_ApplicationOperation_Name UNIQUE NONCLUSTERED
	(
			Name
		,   ApplicationId
	)
GO
