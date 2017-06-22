--IF OBJECT_ID ('dbo.AuditHistory') IS NOT NULL
--BEGIN
--	DROP TABLE dbo.AuditHistory
--END
--GO

CREATE TABLE dbo.AuditHistory 
(
    	AuditHistoryId			INT			IDENTITY (100, 1)	NOT NULL
	,	SystemEntityId			INT								NOT NULL
	,	EntityKey				INT								NOT NULL
	,	AuditActionId			INT								NOT NULL
	,	CreatedDate				DATETIME						NOT NULL
	,	CreatedByPersonId		INT								NOT NULL
	,	TraceId					INT								NULL		
)
GO
