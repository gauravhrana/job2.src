
ALTER TABLE dbo.TaskEntityType
	ADD CONSTRAINT PK_TaskEntityType PRIMARY KEY CLUSTERED 
	(
		TaskEntityTypeId
	)  
GO
ALTER TABLE [dbo].[TaskEntityType] ADD  CONSTRAINT [UQ_TaskEntityType_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)
GO