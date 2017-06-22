IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ForwardFXContract')
	AND		name	= N'UQ_ForwardFXContract_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ForwardFXContract_ApplicationId_Name'
	ALTER	TABLE dbo.ForwardFXContract
	DROP	CONSTRAINT	UQ_ForwardFXContract_ApplicationId_Name
END
GO

ALTER TABLE dbo.ForwardFXContract
ADD CONSTRAINT UQ_ForwardFXContract_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
