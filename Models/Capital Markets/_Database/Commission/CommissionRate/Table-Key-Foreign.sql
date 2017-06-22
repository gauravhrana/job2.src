


ALTER TABLE dbo.CommissionRate
	ADD CONSTRAINT FK_CommissionRate_Broker FOREIGN KEY
	(
		BrokerId
	)
	REFERENCES dbo.Broker
	(
		BrokerId
	)
GO


ALTER TABLE dbo.CommissionRate
	ADD CONSTRAINT FK_CommissionRate_Exchange FOREIGN KEY
	(
		ExchangeId
	)
	REFERENCES dbo.Exchange
	(
		ExchangeId
	)
GO




