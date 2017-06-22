--IF EXISTS
--(
--	SELECT *
--	FROM	dbo.sysindexes
--	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUserProfileImage]')
--	AND		name	= N'UQ_ApplicationUserProfileImage_ApplicationId_ApplicationUserId'
--)
--BEGIN
--	PRINT 'Dropping UQ_ApplicationUserProfileImage_ApplicationId_ApplicationUserId'
--	ALTER TABLE dbo.ApplicationUserProfileImage
--	DROP CONSTRAINT UQ_ApplicationUserProfileImage_ApplicationId_ApplicationUserId
--END
--GO

ALTER TABLE dbo.ApplicationUserProfileImage
	ADD CONSTRAINT UQ_ApplicationUserProfileImage_ApplicationId_ApplicationUserId UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	ApplicationUserId
	)
GO