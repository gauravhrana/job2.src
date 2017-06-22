/******************************************************************************
**		Name: Discount
*******************************************************************************/

EXEC dbo.Discount_Insert @DiscountId = 14	,	@Name = 'Rajat'	,	@Description = 'Cash'		,	@SortOrder = 1   , @Amount = 23 
EXEC dbo.Discount_Insert @DiscountId = 52	,	@Name = 'Raj'	,	@Description = 'Cheque'	    ,   @SortOrder = 2   , @Amount = 24
EXEC dbo.Discount_Insert @DiscountId = 65	,	@Name = 'Mohit'	,	@Description = 'Bank'	    ,	@SortOrder = 3   , @Amount = 25

