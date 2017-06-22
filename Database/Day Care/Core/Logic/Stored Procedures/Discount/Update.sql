IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountUpdate')
BEGIN
	PRINT 'Dropping Procedure DiscountUpdate'
	DROP  Procedure  DiscountUpdate
END
GO

PRINT 'Creating Procedure DiscountUpdate'

GO

/******************************************************************************
**		File: 
**		Name: DiscountUpdate
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

CREATE Procedure dbo.DiscountUpdate
(       
		@DiscountId			INT		
	,	@Name				VARCHAR(50)	
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,	@Amount				INT				= 10
    ,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Discount'
)
AS
BEGIN
	UPDATE	dbo.Discount
	SET		Name					    = @Name			  
		,	Description                 = @Description    
		,	SortOrder					= @SortOrder		
		,	Amount						= @Amount
	WHERE	DiscountId		     		= @DiscountId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @DiscountId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

