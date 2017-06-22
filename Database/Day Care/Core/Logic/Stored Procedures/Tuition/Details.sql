IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionDetails')
BEGIN
	PRINT 'Dropping Procedure TuitionDetails'
	DROP  Procedure TuitionDetails
END
GO

PRINT 'Creating Procedure TuitionDetails'
GO


/******************************************************************************
**		File: 
**		Name: TuitionDetails
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TuitionDetails
(
		@TuitionId				INT   
	,   @AuditId				INT	    = NULL
    ,   @AuditDate				DATETIME	= NULL	 
	,	@SystemEntityType		VARCHAR(50) = 'Tuition'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TuitionId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	TuitionId
		,	ApplicationId
		,	StudentId
		,	TuitionDueDate
		,	TuitionAmount
		,	DiscountId
		,	DiscountAmount
		,	TuitionAmountDue
		,	PaymentMethodId
		,	TuitionPaymentAmount
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'									
	FROM	Tuition 
	WHERE	TuitionId		= @TuitionId 
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   