/******************************************************************************
**		Name: PaymentMethod
*******************************************************************************/

EXEC dbo.PaymentMethod_Search @Name = 'Cheque'
EXEC dbo.PaymentMethod_Search @Name = 'Cash'
EXEC dbo.PaymentMethod_Search @Name = 'Draft'
