--IF OBJECT_ID ('dbo.AuditLog') IS NOT NULL
--	DROP TABLE dbo.AuditLog
--GO

CREATE TABLE dbo.AuditLog
(
	AuditLogId       INT IDENTITY(1,1)	NOT NULL,
	EntryDate        DATETIME			NOT NULL,
	DataMessage      VARCHAR (1000)	    NOT NULL,
	ApplicationId    INT				NOT NULL,
	ConnectionString VARCHAR (1000)		NOT NULL,
	SourceComputer   VARCHAR (50)		NOT NULL
)
GO