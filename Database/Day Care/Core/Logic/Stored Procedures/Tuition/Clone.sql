IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionClone')
BEGIN
	PRINT 'Dropping Procedure TuitionClone'
	DROP  Procedure TuitionClone
END
GO

PRINT 'Creating Procedure TuitionClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TuitionClone
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.TuitionClone
(
		@TuitionId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT						
	,	@StudentId              INT								
	,	@TuitionDueDate			DATETIME						
	,	@TuitionAmount			FLOAT							
	,	@DiscountId             INT								
	,	@DiscountAmount         FLOAT							
	,	@TuitionAmountDue		FLOAT							
	,	@PaymentMethodId		INT								
	,	@TuitionPaymentAmount	FLOAT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Tuition'				
)

AS

BEGIN

	IF @TuitionId IS NULL OR @TuitionId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Tuition', @TuitionId OUTPUT
	END	
		
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@StudentId				=	StudentId
		,	@TuitionDueDate			=	TuitionDueDate
		,	@TuitionAmount			=	TuitionAmount
		,	@DiscountId				=	DiscountId
		,	@DiscountAmount			=	DiscountAmount
		,	@TuitionAmountDue		=	TuitionAmountDue
		,	@PaymentMethodId		=	PaymentMethodId
		,	@TuitionPaymentAmount	=	TuitionPaymentAmount				
	FROM	dbo.Tuition
	WHERE	TuitionId				=	@TuitionId
	AND		ApplicationId			=	@ApplicationId

	EXEC dbo.TuitionInsert 
			@TuitionId				=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@StudentId				=	@StudentId
		,	@TuitionDueDate			=	@TuitionDueDate
		,	@TuitionAmount			=	@TuitionAmount
		,	@DiscountId				=	@DiscountId
		,	@DiscountAmount			=	@DiscountAmount
		,	@TuitionAmountDue		=	@TuitionAmountDue
		,	@PaymentMethodId		=	@PaymentMethodId
		,	@TuitionPaymentAmount	=	@TuitionPaymentAmount
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Tuition'	
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
