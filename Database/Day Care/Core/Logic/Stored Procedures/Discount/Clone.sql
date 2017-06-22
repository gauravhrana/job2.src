IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountClone')
BEGIN
	PRINT 'Dropping Procedure DiscountClone'
	DROP  Procedure DiscountClone
END
GO

PRINT 'Creating Procedure DiscountClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DiscountClone
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

CREATE Procedure dbo.DiscountClone
(
		@DiscountId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@Amount					INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Discount'				
)
AS
BEGIN

	IF @DiscountId IS NULL OR @DiscountId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DiscountId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,	@Amount				= Amount			
	FROM	dbo.Discount
	WHERE	DiscountId		= @DiscountId	
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.DiscountInsert 
			@DiscountId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@Amount				=	@Amount
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @DiscountId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
