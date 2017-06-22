ALTER TABLE dbo.UserPreference
	ADD CONSTRAINT FK_UserPreference_UserPreferenceDataType FOREIGN KEY
	(
		DataTypeId
	)
	REFERENCES UserPreferenceDataType
	(
		UserPreferenceDataTypeId 
	)
GO

ALTER TABLE dbo.UserPreference
	ADD CONSTRAINT FK_UserPreference_UserPreferenceKey FOREIGN KEY
	(
		UserPreferenceKeyId
	)
	REFERENCES UserPreferenceKey
	(
		UserPreferenceKeyId 
	)
GO

ALTER TABLE dbo.UserPreference
	ADD CONSTRAINT FK_UserPreference_UserPreferenceCategory FOREIGN KEY
	(
		UserPreferenceCategoryId
	)
	REFERENCES UserPreferenceCategory
	(
		UserPreferenceCategoryId 
	)
GO
