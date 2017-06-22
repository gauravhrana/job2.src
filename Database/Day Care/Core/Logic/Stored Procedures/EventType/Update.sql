IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure EventTypeUpdate'
	DROP  Procedure  EventTypeUpdate
END
GO

PRINT 'Creating Procedure EventTypeUpdate'

GO

/******************************************************************************
**		File: 
**		Name: EventTypeUpdate
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

CREATE Procedure dbo.EventTypeUpdate
(       
		@EventTypeId		INT		
	,	@Name			    VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId		    INT			
    ,   @AuditDate		    DATETIME        = NULL
	,	@SystemEntityType	VARCHAR(50)	= 'EventType'		
)
AS
BEGIN
	UPDATE	dbo.EventType
	SET		Name					= @Name			
		,	Description             = @Description  
		,	SortOrder				= @SortOrder
	WHERE	EventTypeId		     	= @EventTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EventTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

