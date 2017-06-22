IF OBJECT_ID ('dbo.ApplicationOperationXApplicationRole') IS NOT NULL
	DROP TABLE dbo.ApplicationOperationXApplicationRole
GO


CREATE TABLE dbo.ApplicationOperationXApplicationRole
	(
	ApplicationOperationXApplicationRoleId	INT				NOT NULL,
	ApplicationId							INT				NOT NULL,
	ApplicationOperationId					INT				NOT NULL,
	ApplicationRoleId						INT				NOT NULL
	)
GO