ALTER TABLE dbo.UserPreferenceKey
	ADD CONSTRAINT FK_UserPreferenceKey_UserPreferenceDataType FOREIGN KEY
	(
		DataTypeId
	)
	REFERENCES dbo.UserPreferenceDataType
	(
		UserPreferenceDataTypeId
	)
GO
