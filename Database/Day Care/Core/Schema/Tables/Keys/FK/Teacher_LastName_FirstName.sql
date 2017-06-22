 ALTER TABLE dbo.Teacher
	ADD CONSTRAINT FK_Teacher_LastName FOREIGN KEY 
	(
		LastNameId
	) 
	REFERENCES LastName
	(
		LastNameId
	)
	GO
	ALTER TABLE dbo.Teacher
	ADD CONSTRAINT FK_Teacher_FirstName FOREIGN KEY 
	(
		FirstNameId
	) 
	REFERENCES FirstName
	(
		FirstNameId
	)
GO
	
