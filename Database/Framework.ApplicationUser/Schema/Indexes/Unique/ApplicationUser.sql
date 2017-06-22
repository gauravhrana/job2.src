--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUser]')
--	AND		name	= N'UQ_ApplicationUser_LastName_FirstName'
--)
--BEGIN
--	PRINT 'Dropping UQ_ApplicationUser_LastName'
--	ALTER TABLE dbo.ApplicationUser
--	DROP CONSTRAINT UQ_ApplicationUser_LastName_FirstName
--END
--GO

ALTER TABLE dbo.ApplicationUser
	ADD CONSTRAINT UQ_ApplicationUser_LastName_FirstName UNIQUE NONCLUSTERED
	(
			LastName
		,	FirstName
	)
GO