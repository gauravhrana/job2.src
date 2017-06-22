ALTER TABLE dbo.Student
	ADD CONSTRAINT FK_Student_LastName FOREIGN KEY 
	(
		LastNameId
	) 
	REFERENCES LastName
	(
		LastNameId
	)
	GO
	ALTER TABLE dbo.Student
	ADD CONSTRAINT FK_Student_FirstName FOREIGN KEY 
	(
		FirstNameId
	) 
	REFERENCES FirstName
	(
		FirstNameId
	)
GO
	
