﻿IF OBJECT_ID ('dbo.DBProjectName') IS NOT NULL
	DROP TABLE dbo.DBProjectName
GO

CREATE TABLE dbo.DBProjectName
(
		DBProjectNameId				INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 	
	,	Name					VARCHAR (50)	NOT NULL	
	,	[Description]			VARCHAR (500)	NOT NULL	
	,	SortOrder				INT				NOT NULL
	,	DateCreated				DATETIME		NOT NULL
	,	DateModified			DATETIME		NOT NULL
	,	CreatedByAuditId		INT				NOT NULL
	,	ModifiedByAuditId		INT				NOT NULL
);
