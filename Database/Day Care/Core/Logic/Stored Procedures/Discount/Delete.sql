IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountDelete')
BEGIN
	PRINT 'Dropping Procedure DiscountDelete'
	DROP  Procedure  DiscountDelete
END
GO

PRINT 'Creating Procedure DiscountDelete'
GO

/******************************************************************************
**		File: 
**		Name: DiscountDelete
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

CREATE Procedure dbo.DiscountDelete
(
	    @DiscountId     	INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Discount'
)
AS
BEGIN
	DELETE	dbo.Discount
	WHERE	DiscountId = @DiscountId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @DiscountId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

