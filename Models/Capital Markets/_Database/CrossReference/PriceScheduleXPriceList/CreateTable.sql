IF OBJECT_ID ('dbo.PriceScheduleXPriceList') IS NOT NULL
	DROP TABLE dbo.PriceScheduleXPriceList
GO

CREATE TABLE dbo.PriceScheduleXPriceList
(
		PriceScheduleXPriceListId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	PriceScheduleId			INT		NOT NULL
	,	PriceListId			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
