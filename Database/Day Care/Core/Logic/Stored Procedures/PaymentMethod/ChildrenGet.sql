IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodChildrenGet')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodChildrenGet'
	DROP  Procedure PaymentMethodChildrenGet
END
GO

PRINT 'Creating Procedure PaymentMethodChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: PaymentMethodChildrenGet
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PaymentMethodChildrenGet
(
		@PaymentMethodId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'PaymentMethod'
)
AS
BEGIN

	--GET Tuition Records
	SELECT	a.TuitionId
		,	a.ApplicationId
		,	a.StudentId
		,	a.TuitionDueDate
		,	a.TuitionAmount
		,	a.DiscountId
		,	a.DiscountAmount
		,	a.TuitionAmountDue
		,	a.PaymentMethodId
		,	a.TuitionPaymentAmount	
		,	b.Name					AS 'Discount'		
	FROM		dbo.Tuition	a	
	INNER JOIN	dbo.Discount	b ON a.DiscountId	= b.DiscountId	
	WHERE		a.PaymentMethodId = @PaymentMethodId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   