ALTER TABLE dbo.SickReport
	ADD CONSTRAINT FK_SickReport_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	