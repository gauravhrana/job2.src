﻿ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT PK_TaskEntity PRIMARY KEY CLUSTERED 
	(
		TaskEntityId
	)  
GO
ALTER TABLE [dbo].[TaskEntity] ADD  CONSTRAINT [UQ_TaskEntity_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)
GO