ALTER TABLE dbo.TaskScheduleType
	ADD CONSTRAINT UK_TaskScheduleType UNIQUE 
	(
	ApplicationId,
	Name,
	Description
	)
GO