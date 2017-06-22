IF OBJECT_ID ('dbo.ReleaseNoteQualitative') IS NOT NULL
	DROP TABLE dbo.ReleaseNoteQualitative
GO


CREATE TABLE dbo.ReleaseNoteQualitative
(
		ReleaseNoteQualitativeId	INT				NOT NULL
	,	ApplicationId				INT				NOT NULL
	,	Name						VARCHAR (50)	NOT NULL
	,	Description					VARCHAR (500)	NOT NULL
	,	SortOrder					INT				NOT NULL
	,	DateCreated					DATETIME		NOT NULL
	,	DateModified				DATETIME		NOT NULL
	,	CreatedByAuditId			INT				NOT NULL
	,	ModifiedByAuditId			INT				NOT NULL
);

