


ALTER TABLE dbo.CommissionCode
	ADD CONSTRAINT FK_CommissionCode_Broker FOREIGN KEY
	(
		BrokerId
	)
	REFERENCES dbo.Broker
	(
		BrokerId
	)
GO




