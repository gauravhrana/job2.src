ALTER TABLE dbo.UserPreferenceSelectedItem
	ADD CONSTRAINT FK_UserPreferenceSelectedItem_UserPreferenceKey FOREIGN KEY
	(
		UserPreferenceKeyId
	)
	REFERENCES dbo.UserPreferenceKey
	(
		UserPreferenceKeyId
	)
GO
