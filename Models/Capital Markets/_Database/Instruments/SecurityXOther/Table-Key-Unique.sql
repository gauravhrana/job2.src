IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityXOther')
	AND		name	= N'UQ_SecurityXOther_ApplicationId_SecurityId'
)
BEGIN
	PRINT	'Dropping UQ_SecurityXOther_ApplicationId_SecurityId'
	ALTER	TABLE dbo.SecurityXOther
	DROP	CONSTRAINT	UQ_SecurityXOther_ApplicationId_SecurityId
END
GO

ALTER TABLE dbo.SecurityXOther
ADD CONSTRAINT UQ_SecurityXOther_ApplicationId_SecurityId UNIQUE NONCLUSTERED
(
	ApplicationId, SecurityId
)
GO
