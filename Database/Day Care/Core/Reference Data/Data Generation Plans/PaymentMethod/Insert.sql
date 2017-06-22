/******************************************************************************
**		Name: PaymentMethod
*******************************************************************************/

EXEC dbo.PaymentMethod_Insert @PaymentMethodId = 44	,	@Name = 'Tom'	,	@Description = 'Bank'		,	@SortOrder = 1
EXEC dbo.PaymentMethod_Insert @PaymentMethodId = 55	,	@Name = 'Rom'	,	@Description = 'Cheque'	    ,   @SortOrder = 2
EXEC dbo.PaymentMethod_Insert @PaymentMethodId = 64	,	@Name = 'Sam'	,	@Description = 'Draft'	    ,	@SortOrder = 3

