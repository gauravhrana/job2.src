/******************************************************************************
**		Name: Tuition
*******************************************************************************/

EXEC dbo.Tuition_Update @TuitionId = -112	,	@StudentId = 11,	@TuitionDueDate = '1/1/2012',	@TuitionAmount = 1002,   @DiscountId = 2,   @DiscountAmount = 75,		@TuitionAmoutDue = 56,	@PaymentMethod = 'Cash',		@TuitionPaymentAmount = 'Yes'
EXEC dbo.Tuition_Update @TuitionId = -218	,	@StudentId = 41,	@TuitionDueDate = '6/1/2012',	@TuitionAmount = 2003,   @DiscountId = 5,   @DiscountAmount = 86,		@TuitionAmoutDue = 57,	@PaymentMethod = 'Cheque',		@TuitionPaymentAmount = 'Full'
EXEC dbo.Tuition_Update @TuitionId = -119	,	@StudentId = 16,	@TuitionDueDate = '2/2/2012',	@TuitionAmount = 5008,   @DiscountId = 8,   @DiscountAmount = 58,		@TuitionAmoutDue = 68,	@PaymentMethod = 'Draft',		@TuitionPaymentAmount = 'Half'

