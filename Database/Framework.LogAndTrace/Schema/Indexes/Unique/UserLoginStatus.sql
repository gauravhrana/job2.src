IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserLoginStatus]')
	AND		name	= N'UQ_UserLoginStatus_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_UserLoginStatus_Name_ApplicationId'
	ALTER	TABLE dbo.UserLoginStatus
	DROP	CONSTRAINT	UQ_UserLoginStatus_Name_ApplicationId
END
GO

ALTER TABLE dbo.UserLoginStatus
ADD CONSTRAINT UQ_UserLoginStatus_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
