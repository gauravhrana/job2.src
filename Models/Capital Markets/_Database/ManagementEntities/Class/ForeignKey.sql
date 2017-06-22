
ALTER TABLE dbo.Class
	ADD CONSTRAINT FK_Class_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







