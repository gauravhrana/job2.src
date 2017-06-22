IF OBJECT_Id ('dbo.Discount') IS NOT NULL
   DROP TABLE dbo.Discount
GO

CREATE TABLE dbo.Discount
(
		DiscountId		INT           NOT NULL
	,	ApplicationId   INT           NOT NULL
	,	Name			VARCHAR (50)  NOT NULL
	,	Description		VARCHAR (500) NOT NULL
	,	SortOrder		INT           NOT NULL
	,	Amount			INT           NOT NULL
)
GO

