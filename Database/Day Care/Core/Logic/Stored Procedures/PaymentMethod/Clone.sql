IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodClone')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodClone'
	DROP  Procedure PaymentMethodClone
END
GO

PRINT 'Creating Procedure PaymentMethodClone'
GO

/*********************************************************************************************
**		File: 
**		Name: PaymentMethodClone
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

CREATE Procedure dbo.PaymentMethodClone
(
		@PaymentMethodId		INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT						
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'PaymentMethod'				
)

AS

BEGIN

	IF @PaymentMethodId IS NULL
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'PaymentMethod', @PaymentMethodId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.PaymentMethod
	WHERE	PaymentMethodId	= @PaymentMethodId  
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.PaymentMethodInsert 
			@PaymentMethodId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'PaymentMethod'	
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
