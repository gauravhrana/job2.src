IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodDetails')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodDetails'
	DROP  Procedure PaymentMethodDetails
END
GO

PRINT 'Creating Procedure PaymentMethodDetails'
GO


/******************************************************************************
**		File: 
**		Name: PaymentMethodDetails
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

CREATE Procedure dbo.PaymentMethodDetails
(
		@PaymentMethodId    INT		
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME    = NULL
	,   @SystemEntityType	VARCHAR(50)	= 'PaymentMethod'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@PaymentMethodId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	PaymentMethodId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	PaymentMethod 
	WHERE	PaymentMethodId = @PaymentMethodId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   