ALTER TABLE dbo.BatchFileStatus
	ADD CONSTRAINT PK_BatchFileStatus PRIMARY KEY CLUSTERED 
	(
		BatchFileStatusId
	)  
GO
ALTER TABLE dbo.BatchFileStatus
ADD  CONSTRAINT [UQ_BatchFileStatus_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)
GO