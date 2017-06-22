
ALTER TABLE dbo.Series
	ADD CONSTRAINT FK_Series_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







