
ALTER TABLE dbo.BatchFileHistory
	ADD CONSTRAINT FK_BatchFileHistory_BatchFileSet FOREIGN KEY
	(
		BatchFileSetId
	)
	REFERENCES BatchFileSet
	(
		BatchFileSetId 
	)
GO

ALTER TABLE dbo.BatchFileHistory
	ADD CONSTRAINT FK_BatchFileHistory_BatchFile FOREIGN KEY
	(
		BatchFileId
	)
	REFERENCES BatchFile
	(
		BatchFileId 
	)
GO

ALTER TABLE dbo.BatchFileHistory
	ADD CONSTRAINT FK_BatchFileHistory_BatchFileStatus FOREIGN KEY
	(
		BatchFileStatusId
	)
	REFERENCES BatchFileStatus
	(
		BatchFileStatusId 
	)
GO

ALTER TABLE dbo.BatchFileHistory
	ADD CONSTRAINT FK_BatchFileHistory_Person FOREIGN KEY
	(
		UpdatedByPersonId
	)
	REFERENCES Person
	(
		PersonId 
	)
GO