
CREATE TABLE dbo.ApplicationUser 
(
		ApplicationUserId		INT				NOT NULL
	,	ApplicationId			INT				NOT NULL
	,	ApplicationUserTitleId	INT				NOT NULL
	,	ApplicationUserName		VARCHAR (50)	NOT NULL
	,	FirstName				VARCHAR (50)	NOT NULL
	,	LastName				VARCHAR (50)	NOT NULL
	,	MiddleName				VARCHAR (50)	NOT NULL
	,	EmailAddress			VARCHAR (320)	NOT NULL
)