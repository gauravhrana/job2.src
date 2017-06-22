ALTER TABLE dbo.TaskRun
	ADD CONSTRAINT FK_TaskRun_TaskSchedule FOREIGN KEY
	(
		TaskScheduleId
	)
	REFERENCES TaskSchedule
	(
		TaskScheduleId
	)
GO

ALTER TABLE dbo.TaskRun
	ADD CONSTRAINT FK_TaskRun_TaskEntity FOREIGN KEY
	(
		TaskEntityId
	)
	REFERENCES TaskEntity
	(
		TaskEntityId 
	)
GO

