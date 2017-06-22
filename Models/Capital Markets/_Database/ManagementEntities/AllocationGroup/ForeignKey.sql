
ALTER TABLE dbo.AllocationGroup
	ADD CONSTRAINT FK_AllocationGroup_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







