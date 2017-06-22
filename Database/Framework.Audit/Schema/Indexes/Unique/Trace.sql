--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'dbo.Trace')
--	AND		name	= N'UQ_Trace_Name_ApplicationId'
--)
--BEGIN
--	PRINT	'Dropping UQ_Trace_Name_ApplicationId'
--	ALTER TABLE dbo.Trace
--		DROP CONSTRAINT	UQ_Trace_Name_ApplicationId
--END
--GO

ALTER TABLE dbo.Trace
	ADD CONSTRAINT UQ_Trace_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
