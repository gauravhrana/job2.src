IF OBJECT_ID ('dbo.DeveloperRole') IS NOT NULL
	DROP TABLE dbo.DeveloperRole
GO

CREATE TABLE dbo.DeveloperRole
(
		DeveloperRoleId			INT				NOT NULL
	,	ApplicationId			INT				NOT NULL
	,	Name					VARCHAR (50)	NOT NULL	
	,	Description				VARCHAR (50)	NOT NULL	
	,	SortOrder				INT				NOT NULL
	,	DateCreated				DATETIME		NOT NULL
	,	DateModified			DATETIME		NOT NULL
	,	CreatedByAuditId		INT				NOT NULL
	,	ModifiedByAuditId		INT				NOT NULL
);

