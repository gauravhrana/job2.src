IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemList')
BEGIN
	PRINT 'Dropping Procedure NeedItemList'
	DROP PROCEDURE NeedItemList
END
GO

PRINT 'Creating Procedure NeedItemList'
GO

/******************************************************************************
**		File: 
**		Name: NeedItemList
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

CREATE Procedure dbo.NeedItemList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NeedItem'
)
AS
BEGIN
		SELECT	NeedItemId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.NeedItem 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY NeedItemId			ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
