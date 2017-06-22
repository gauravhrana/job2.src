ALTER TABLE dbo.PersonXApplicationRole
	ADD CONSTRAINT FK_PersonXApplicationRole_Person FOREIGN KEY
	(
		PersonId
	)
	REFERENCES Person
	(
		PersonId
	)
GO

ALTER TABLE dbo.PersonXApplicationRole
	ADD CONSTRAINT FK_PersonXApplicationRole_ApplicationRole FOREIGN KEY
	(
		ApplicationRoleId
	)
	REFERENCES ApplicationRole
	(
		ApplicationRoleId
	)
GO
