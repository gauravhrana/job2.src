IF OBJECT_ID ('dbo.ApplicationRole') IS NOT NULL
	DROP TABLE dbo.ApplicationRole
GO

CREATE TABLE dbo.ApplicationRole
(
		ApplicationRoleId INT				NOT NULL
	,	Name              VARCHAR (50)		NOT NULL
	,	Description       VARCHAR (500)		NOT NULL
	,	SortOrder         INT				NOT NULL
)
GO
