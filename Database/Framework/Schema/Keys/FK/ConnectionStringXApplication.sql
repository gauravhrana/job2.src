ALTER TABLE dbo.ConnectionStringXApplication
	ADD CONSTRAINT FK_ConnectionStringXApplication_ConnectionString FOREIGN KEY
	(
		ConnectionStringId
	)
	REFERENCES dbo.ConnectionString
	(
		ConnectionStringId 
	)
GO