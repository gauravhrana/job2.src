CREATE TABLE dbo.ApplicationUserProfileImage 
(
		ApplicationUserProfileImageId	INT				NOT NULL
	,	ApplicationId					INT				NOT NULL
	,	ApplicationUserId				INT				NOT NULL
	,	Image							VARBINARY(MAX)	NOT NULL
);