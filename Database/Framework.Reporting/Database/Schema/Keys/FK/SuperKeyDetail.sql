ALTER TABLE dbo.SuperKeyDetail
	ADD CONSTRAINT FK_SuperKeyDetail_SuperKey FOREIGN KEY
	(
		SuperKeyId
	)
	REFERENCES dbo.SuperKey
	(
		SuperKeyId 
	)
GO