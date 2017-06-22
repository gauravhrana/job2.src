ALTER TABLE dbo.SearchKeyDetailItem
	ADD CONSTRAINT FK_SearchKeyDetailItem_SearchKeyDetail FOREIGN KEY
	(
		SearchKeyDetailId
	)
	REFERENCES dbo.SearchKeyDetail
	(
		SearchKeyDetailId 
	)
GO