IF OBJECT_ID ('dbo.MSPAFile') IS NOT NULL
	DROP TABLE dbo.MSPAFile
GO

CREATE TABLE dbo.MSPAFile
(
		MSPAFileId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Filename				VARCHAR(100)		NOT NULL
	,	DropDate				DATETIME		NULL
	,	BusinessDate				DATETIME		NULL
	,	MSPAExtractTaskRunId				INT		NOT NULL
	,	MSPAHoldingTaskRunId				INT		NOT NULL
	,	MSPATradeTaskRunId				INT		NOT NULL
	,	MSPASecurityTaskRunId				INT		NOT NULL
)
GO
