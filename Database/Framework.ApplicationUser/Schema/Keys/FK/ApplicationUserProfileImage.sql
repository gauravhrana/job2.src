ALTER TABLE dbo.ApplicationUserProfileImage
	ADD CONSTRAINT FK_ApplicationUserProfileImage_ApplicationUser FOREIGN KEY
	(
		ApplicationUserId
	)
	REFERENCES dbo.ApplicationUser
	(
		ApplicationUserId
	)
GO


