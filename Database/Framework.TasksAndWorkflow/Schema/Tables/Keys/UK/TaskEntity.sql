ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT UK_TaskEntity UNIQUE 
	(
	ApplicationId,
	Name
	)
GO