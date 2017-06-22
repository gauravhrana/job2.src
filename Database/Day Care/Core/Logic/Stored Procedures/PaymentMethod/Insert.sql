IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodInsert')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodInsert'
	DROP  Procedure PaymentMethodInsert
END
GO

PRINT 'Creating Procedure PaymentMethodInsert'
GO

/******************************************************************************
**		File: 
**		Name: pPaymentMethodInsert
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
CREATE Procedure dbo.PaymentMethodInsert
(
		@PaymentMethodId		INT
	,	@ApplicationId			INT						
	,	@Name					VARCHAR(50)
	,	@Description			VARCHAR(500)	= NULL
	,	@SortOrder				INT				= 1
	,   @AuditId				INT			
    ,   @AuditDate				DATETIME = NULL
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @PaymentMethodId OUTPUT
		
	INSERT INTO dbo.PaymentMethod
	(
			PaymentMethodId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@PaymentMethodId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
	)

    --Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'PaymentMethod' 
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
