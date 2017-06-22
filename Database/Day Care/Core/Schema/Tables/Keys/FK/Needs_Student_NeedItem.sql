ALTER TABLE dbo.Needs
	ADD CONSTRAINT FK_Needs_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Needs
	ADD CONSTRAINT FK_Needs_NeedItem FOREIGN KEY 
	(
		NeedItemId
	) 
	REFERENCES NeedItem
	(
		NeedItemId
	)
GO
	
