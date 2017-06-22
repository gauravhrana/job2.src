IF OBJECT_ID ('dbo.EventSubType') IS NOT NULL
	DROP TABLE dbo.EventSubType
GO

CREATE TABLE dbo.EventSubType
(
		EventSubTypeId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	EventTypeId			INT		NOT NULL
	,	PersonId			INT		NOT NULL
	,	EventKey				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	Value			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
