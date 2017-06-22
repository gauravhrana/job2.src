--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUserProfileImageMaster]')
--	AND		name	= N'UQ_ApplicationUserProfileImageMaster_ApplicationId_Title'
--)
--BEGIN
--	PRINT 'Dropping UQ_ApplicationUserProfileImageMaster_ApplicationId_Title'
--	ALTER TABLE dbo.ApplicationUserProfileImageMaster
--	DROP CONSTRAINT UQ_ApplicationUserProfileImageMaster_ApplicationId_Title
--END
--GO

ALTER TABLE dbo.ApplicationUserProfileImageMaster
	ADD CONSTRAINT UQ_ApplicationUserProfileImageMaster_ApplicationId_Title UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	Title
	)
GO