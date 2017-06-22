IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionInsert')
BEGIN
	PRINT 'Dropping ProcedureTuitionInsert'
	DROP  Procedure TuitionInsert
END

GO

PRINT 'Creating ProcedureTuitionInsert'
GO

/******************************************************************************
**		File: 
**		Name: TuitionInsert
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TuitionInsert
(
		@TuitionId                INT      
	,	@ApplicationId			  INT
	,	@StudentId                INT      
	,	@TuitionDueDate			  DATETIME 
	,	@TuitionAmount			  FLOAT    
	,	@DiscountId               INT      
	,	@DiscountAmount           FLOAT    
	,	@TuitionAmountDue		  FLOAT    
	,	@PaymentMethodId		  INT      
	,	@TuitionPaymentAmount	  FLOAT    
	,   @AuditId                  INT	   
    ,   @AuditDate                DATETIME	 = NULL
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TuitionId OUTPUT, @AuditId
		
	INSERT INTO dbo.Tuition
	(
			TuitionId
		,	ApplicationId
		,	StudentId
		,	TuitionDueDate
		,	TuitionAmount
		,	DiscountId
		,	DiscountAmount
		,	TuitionAmountDue
		,	PaymentMethodId
		,	TuitionPaymentAmount
	)
	VALUES
	(
			@TuitionId
		,	@ApplicationId
		,	@StudentId
		,	@TuitionDueDate
		,	@TuitionAmount
		,	@DiscountId
		,	@DiscountAmount
		,	@TuitionAmountDue
		,	@PaymentMethodId
		,	@TuitionPaymentAmount
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Tuition'
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO
