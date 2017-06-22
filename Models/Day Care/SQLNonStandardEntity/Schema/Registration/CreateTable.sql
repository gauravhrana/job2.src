IF OBJECT_ID ('dbo.Registration') IS NOT NULL
	DROP TABLE dbo.Registration
GO

CREATE TABLE dbo.Registration
(
		RegistrationId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	CourseId			INT		NOT NULL
	,	StudentId			INT		NOT NULL
	,	EnrollmentDate				DATETIME		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
