IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ReleaseLogDetail')
	AND		name	= N'UQ_ReleaseLogDetail_ReleaseLogId_ApplicationId_ItemNo'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseLogDetail_ReleaseLogId_ApplicationId_ItemNo'
	ALTER TABLE dbo.ReleaseLogDetail
		DROP CONSTRAINT	UQ_ReleaseLogDetail_ReleaseLogId_ApplicationId_ItemNo
END
GO

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT UQ_ReleaseLogDetail_ReleaseLogId_ApplicationId_ItemNo UNIQUE NONCLUSTERED
	(
			ReleaseLogId
		,	ApplicationId
		,	ItemNo
	)
GO
