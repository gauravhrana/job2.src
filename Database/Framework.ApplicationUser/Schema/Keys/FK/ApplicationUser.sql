ALTER TABLE dbo.ApplicationUser
	ADD CONSTRAINT FK_ApplicationUser_ApplicationUserTitle FOREIGN KEY
	(
		ApplicationUserTitleId
	)
	REFERENCES ApplicationUserTitle
	(
		ApplicationUserTitleId
	)
GO


