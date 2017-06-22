









ALTER TABLE dbo.CommissionSplit
	ADD CONSTRAINT FK_CommissionSplit_CommissionCode FOREIGN KEY
	(
		CommissionCodeId
	)
	REFERENCES dbo.CommissionCode
	(
		CommissionCodeId
	)
GO




