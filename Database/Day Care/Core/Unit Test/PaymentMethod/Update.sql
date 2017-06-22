/******************************************************************************
**		Name: PaymentMethod
*******************************************************************************/

EXEC dbo.PaymentMethod_Update @PaymentMethodId = 442	,	@Name = 'Ranny'	,	@Description = 'Cash'		,	@SortOrder = 11
EXEC dbo.PaymentMethod_Update @PaymentMethodId = 552	,	@Name = 'Sanny'	,	@Description = 'Cheque'	    ,   @SortOrder = 21
EXEC dbo.PaymentMethod_Update @PaymentMethodId = 642	,	@Name = 'Fanny'	,	@Description = 'Bank'	    ,	@SortOrder = 31

