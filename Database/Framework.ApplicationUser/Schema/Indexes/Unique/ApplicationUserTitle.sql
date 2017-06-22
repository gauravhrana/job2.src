--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUserTitle]')
--	AND		name	= N'UQ_ApplicationUserTitle_Name_ApplicationId'
--)
--BEGIN
--	PRINT 'Dropping UQ_ApplicationUserTitle_Name_ApplicationId'
--ALTER TABLE dbo.ApplicationUserTitle
--	DROP CONSTRAINT UQ_ApplicationUserTitle_Name_ApplicationId
--END
--GO

ALTER TABLE dbo.ApplicationUserTitle
	ADD CONSTRAINT UQ_ApplicationUserTitle_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO