ALTER TABLE dbo.BatchFileSet
	ADD CONSTRAINT FK_BatchFileSet_Person FOREIGN KEY
	(
		CreatedByPersonId
	)
	REFERENCES Person
	(
		PersonId 
	)
GO