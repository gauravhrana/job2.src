ALTER TABLE dbo.TaskSchedule
	ADD CONSTRAINT FK_TaskSchedule_TaskScheduleType FOREIGN KEY
	(
		TaskScheduleTypeId
	)
	REFERENCES TaskScheduleType
	(
		TaskScheduleTypeId 
	)
GO

ALTER TABLE dbo.TaskSchedule
	ADD CONSTRAINT FK_TaskSchedule_TaskEntity FOREIGN KEY
	(
		TaskEntityId
	)
	REFERENCES TaskEntity
	(
		TaskEntityId 
	)
GO