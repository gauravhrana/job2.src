ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT FK_TaskEntity_TaskEntityType FOREIGN KEY
	(
		TaskEntityTypeId
	)
	REFERENCES TaskEntityType
	(
		TaskEntityTypeId 
	)
GO