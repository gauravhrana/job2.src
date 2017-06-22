CREATE TABLE dbo.ApplicationUserProfileImageMaster 
(
		ApplicationUserProfileImageMasterId	INT				NOT NULL
	,	ApplicationId						INT				NOT NULL
	,	Title								VARCHAR(50)		NOT NULL
	,	Image								VARBINARY(MAX)	NOT NULL
);
