
IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseLogdetail_TimeSpent'
)

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT DF_ReleaseLogdetail_TimeSpent		DEFAULT 'UnKnown' 	FOR TimeSpent
GO