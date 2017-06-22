
ALTER TABLE dbo.ApplicationUserXApplicationRole
	ADD CONSTRAINT FK_ApplicationUserXApplicationRole_ApplicationUser FOREIGN KEY
	(
		ApplicationUserId
	)
	REFERENCES dbo.ApplicationUser
	(
		ApplicationUserId
	)
GO


ALTER TABLE dbo.ApplicationUserXApplicationRole
	ADD CONSTRAINT FK_ApplicationUserXApplicationRole_ApplicationRole FOREIGN KEY
	(
		ApplicationRoleId
	)
	REFERENCES dbo.ApplicationRole
	(
		ApplicationRoleId
	)
GO