ALTER TABLE dbo.Comment
	ADD CONSTRAINT FK_Comment_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Comment
	ADD CONSTRAINT FK_Comment_EventType FOREIGN KEY 
	(
		EventTypeId
	) 
	REFERENCES EventType
	(
		EventTypeId
	)
GO
	
