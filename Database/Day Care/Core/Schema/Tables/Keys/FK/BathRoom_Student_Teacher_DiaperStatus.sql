ALTER TABLE dbo.Bathroom
	ADD CONSTRAINT FK_Bathroom_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Bathroom
	ADD CONSTRAINT FK_Bathroom_Teacher FOREIGN KEY 
	(
		TeacherId
	) 
	REFERENCES Teacher
	(
		TeacherId
	)
GO
ALTER TABLE dbo.Bathroom
	ADD CONSTRAINT FK_Bathroom_DiaperStatus FOREIGN KEY 
	(
		DiaperStatusId
	) 
	REFERENCES DiaperStatus
	(
		DiaperStatusId
	)
GO
	
	
