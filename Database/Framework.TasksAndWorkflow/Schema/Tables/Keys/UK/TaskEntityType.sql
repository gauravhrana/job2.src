ALTER TABLE dbo.TaskEntityType
	ADD CONSTRAINT UK_TaskEntityType UNIQUE 
	(
	ApplicationId,
	Name
	)
GO