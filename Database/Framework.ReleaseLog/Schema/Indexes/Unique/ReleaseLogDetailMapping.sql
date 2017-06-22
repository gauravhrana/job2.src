IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ReleaseLogDetailMapping')
	AND		name	= N'UQ_ReleaseLogDetailMapping_ParentReleaseLogDetailId_ChildReleaseLogDetailId_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseLogDetailMapping_ParentReleaseLogDetailId_ChildReleaseLogDetailId_ApplicationId'
	ALTER TABLE dbo.ReleaseLogDetailMapping
		DROP CONSTRAINT	UQ_ReleaseLogDetailMapping_ParentReleaseLogDetailId_ChildReleaseLogDetailId_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseLogDetailMapping
	ADD CONSTRAINT UQ_ReleaseLogDetailMapping_ParentReleaseLogDetailId_ChildReleaseLogDetailId_ApplicationId UNIQUE NONCLUSTERED
	(
			ParentReleaseLogDetailId
		,	ChildReleaseLogDetailId
		,	ApplicationId		
	)
GO
