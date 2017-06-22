IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[PaymentMethod]')
	AND		name	= N'UQ_PaymentMethod_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PaymentMethod_ApplicationId_Name'
	ALTER	TABLE dbo.PaymentMethod
	DROP	CONSTRAINT	UQ_PaymentMethod_ApplicationId_Name
END
GO

ALTER TABLE dbo.PaymentMethod
ADD CONSTRAINT UQ_PaymentMethod_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
