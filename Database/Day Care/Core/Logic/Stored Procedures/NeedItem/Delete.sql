IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemDelete')
BEGIN
	PRINT 'Dropping Procedure NeedItemDelete'
	DROP  Procedure  NeedItemDelete
END
GO

PRINT 'Creating Procedure NeedItemDelete'
GO

/******************************************************************************
**		File: 
**		Name: NeedItemDelete
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

CREATE Procedure dbo.NeedItemDelete
(
	    @NeedItemId	        INT	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,   @SystemEntityType	VARCHAR(50)	= 'NeedItem'				
)
AS
BEGIN
	DELETE	dbo.NeedItem
	WHERE	NeedItemId = @NeedItemId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NeedItemId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

