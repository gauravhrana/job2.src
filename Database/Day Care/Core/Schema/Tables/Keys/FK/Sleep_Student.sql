ALTER TABLE dbo.Sleep
	ADD CONSTRAINT FK_Sleep_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	