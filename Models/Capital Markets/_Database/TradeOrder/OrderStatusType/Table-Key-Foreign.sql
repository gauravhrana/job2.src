


ALTER TABLE dbo.OrderStatusType
	ADD CONSTRAINT FK_OrderStatusType_OrderStatusGroup FOREIGN KEY
	(
		OrderStatusGroupId
	)
	REFERENCES dbo.OrderStatusGroup
	(
		OrderStatusGroupId
	)
GO




