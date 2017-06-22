IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountList')
BEGIN
	PRINT 'Dropping Procedure DiscountList'
	DROP PROCEDURE DiscountList
END
GO

PRINT 'Creating Procedure DiscountList'
GO

/******************************************************************************
**		File: 
**		Name: DiscountList
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

CREATE Procedure dbo.DiscountList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Discount'
)
AS
BEGIN
		SELECT	DiscountId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
			,	Amount
		FROM	dbo.Discount 
		WHERE ApplicationId			= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY DiscountId			ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
