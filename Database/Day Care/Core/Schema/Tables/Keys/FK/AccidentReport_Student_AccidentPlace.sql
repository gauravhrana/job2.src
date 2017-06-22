ALTER TABLE dbo.AccidentReport
	ADD CONSTRAINT FK_AccidentReport_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.AccidentReport
	ADD CONSTRAINT FK_AccidentReport_AccidentPlace FOREIGN KEY 
	(
		AccidentPlaceId
	) 
	REFERENCES AccidentPlace
	(
		AccidentPlaceId
	)
GO
	
