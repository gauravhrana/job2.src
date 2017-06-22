IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='DiscountDelete') 
	BEGIN
	DROP Procedure DiscountDelete
END
GO

CREATE Procedure dbo.DiscountDelete
(
		@DiscountId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Discount'
)
AS
BEGIN
		DELETE dbo.Discount
		WHERE	DiscountId = @DiscountId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @DiscountId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
