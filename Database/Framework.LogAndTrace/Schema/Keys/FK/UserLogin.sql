ALTER TABLE dbo.UserLogin
	ADD CONSTRAINT FK_UserLogin_UserLoginStatus FOREIGN KEY
	(
		UserLoginStatusId
	)
	REFERENCES dbo.UserLoginStatus
	(
		UserLoginStatusId
	)
GO
