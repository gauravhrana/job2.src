IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemUpdate')
BEGIN
	PRINT 'Dropping Procedure NeedItemUpdate'
	DROP  Procedure  NeedItemUpdate
END
GO

PRINT 'Creating Procedure NeedItemUpdate'

GO

/******************************************************************************
**		File: 
**		Name: NeedItemUpdate
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

CREATE Procedure dbo.NeedItemUpdate
(       
		@NeedItemId		    INT			
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)	    = 'NeedItem'	
)
AS
BEGIN
	UPDATE	dbo.NeedItem
	SET		Name				=	@Name
		,	Description			=	@Description
		,	SortOrder			=	@SortOrder
	WHERE	NeedItemId		    =   @NeedItemId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NeedItemId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

