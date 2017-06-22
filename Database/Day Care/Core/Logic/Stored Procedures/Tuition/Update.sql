IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionUpdate')
BEGIN
	PRINT 'Dropping Procedure TuitionUpdate'
	DROP  Procedure  TuitionUpdate
END
GO

PRINT 'Creating Procedure TuitionUpdate'

GO

/******************************************************************************
**		File: 
**		Name: TuitionUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.TuitionUpdate
(       
		@TuitionId                INT	
	,	@StudentId                INT      
	,	@TuitionDueDate			  DATETIME 
	,	@TuitionAmount			  FLOAT    
	,	@DiscountId               INT      
	,	@DiscountAmount           FLOAT    
	,	@TuitionAmountDue		  FLOAT    
	,	@PaymentMethodId		  INT      
	,	@TuitionPaymentAmount	  FLOAT    
	,	@AuditId                  INT	   
    ,   @AuditDate                DATETIME		= NULL
	,   @SystemEntityType		  VARCHAR(50)	= 'Tuition'
	 
)
AS
BEGIN

	UPDATE	dbo.Tuition
	SET		TuitionId			=		@TuitionId	
		,	StudentId			=		@StudentId
		,	TuitionDueDate		=		@TuitionDueDate
		,	TuitionAmount		=		@TuitionAmount
		,	DiscountId			=		@DiscountId
		,	DiscountAmount		=		@DiscountAmount
		,	TuitionAmountDue	=		@TuitionAmountDue
		,	PaymentMethodId		=		@PaymentMethodId
		,	TuitionPaymentAmount	=	@TuitionPaymentAmount
	WHERE	TuitionId		     	=	@TuitionId
		
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType	    = @SystemEntityType
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

