ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_FileType FOREIGN KEY
	(
		FileTypeId
	)
	REFERENCES FileType
	(
		FileTypeId
	)
GO

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_BatchFileStatus FOREIGN KEY
	(
		BatchFileStatusId
	)
	REFERENCES BatchFileStatus
	(
		BatchFileStatusId 
	)
GO

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_SystemEntityType FOREIGN KEY
	(
		SystemEntityTypeId
	)
	REFERENCES SystemEntityType
	(
		SystemEntityTypeId 
	)
GO

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_Person_Created FOREIGN KEY
	(
		CreatedByPersonId
	)
	REFERENCES Person
	(
		PersonId 
	)
GO

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_Person_Updated FOREIGN KEY
	(
		UpdatedByPersonId
	)
	REFERENCES Person
	(
		PersonId 
	)
GO

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT FK_BatchFile_BatchFileSet FOREIGN KEY
	(
		BatchFileSetId
	)
	REFERENCES BatchFileSet
	(
		BatchFileSetId 
	)
GO
