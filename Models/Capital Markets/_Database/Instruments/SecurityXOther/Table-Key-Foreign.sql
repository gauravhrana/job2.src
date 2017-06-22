


ALTER TABLE dbo.SecurityXOther
	ADD CONSTRAINT FK_SecurityXOther_Security FOREIGN KEY
	(
		SecurityId
	)
	REFERENCES dbo.Security
	(
		SecurityId
	)
GO




