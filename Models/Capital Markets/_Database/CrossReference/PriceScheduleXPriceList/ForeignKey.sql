
ALTER TABLE dbo.PriceScheduleXPriceList
	ADD CONSTRAINT FK_PriceScheduleXPriceList_PriceSchedule FOREIGN KEY
	(
		PriceScheduleId
	)
	REFERENCES dbo.PriceSchedule
	(
		PriceScheduleId
	)
GO

ALTER TABLE dbo.PriceScheduleXPriceList
	ADD CONSTRAINT FK_PriceScheduleXPriceList_PriceList FOREIGN KEY
	(
		PriceListId
	)
	REFERENCES dbo.PriceList
	(
		PriceListId
	)
GO





