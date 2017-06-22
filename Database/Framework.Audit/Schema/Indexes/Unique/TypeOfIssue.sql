--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'dbo.TypeOfIssue')
--	AND		name	= N'UQ_TypeOfIssue_Name'
--)
--BEGIN
--	PRINT	'Dropping UQ_TypeOfIssueOperation_Name'
--	ALTER TABLE dbo.TypeOfIssue
--		DROP CONSTRAINT	UQ_TypeOfIssue_Name
--END
--GO

ALTER TABLE dbo.TypeOfIssue
	ADD CONSTRAINT UQ_TypeOfIssue_Name UNIQUE NONCLUSTERED
	(
		Name
	)
GO
