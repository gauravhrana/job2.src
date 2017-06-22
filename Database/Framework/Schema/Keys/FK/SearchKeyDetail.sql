ALTER TABLE dbo.SearchKeyDetail
	ADD CONSTRAINT FK_SearchKeyDetail_SearchKey FOREIGN KEY
	(
		SearchKeyId
	)
	REFERENCES dbo.SearchKey
	(
		SearchKeyId 
	)
GO