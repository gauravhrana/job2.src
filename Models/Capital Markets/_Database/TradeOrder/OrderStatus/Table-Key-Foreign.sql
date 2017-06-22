




ALTER TABLE dbo.OrderStatus
	ADD CONSTRAINT FK_OrderStatus_OrderStatusType FOREIGN KEY
	(
		OrderStatusTypeId
	)
	REFERENCES dbo.OrderStatusType
	(
		OrderStatusTypeId
	)
GO




