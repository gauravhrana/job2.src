
ALTER TABLE dbo.Fund
	ADD CONSTRAINT FK_Fund_ManagementFirm FOREIGN KEY
	(
		ManagementFirmId
	)
	REFERENCES dbo.ManagementFirm
	(
		ManagementFirmId
	)
GO







