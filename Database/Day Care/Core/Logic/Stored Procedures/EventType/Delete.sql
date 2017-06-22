IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeDelete')
BEGIN
	PRINT 'Dropping Procedure EventTypeDelete'
	DROP  Procedure  EventTypeDelete
END
GO

PRINT 'Creating Procedure EventTypeDelete'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeDelete
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

CREATE Procedure dbo.EventTypeDelete
(
	    @EventTypeId			INT
	,   @AuditId				INT			
    ,   @AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'EventType'
)
AS
 BEGIN
	DELETE	dbo.EventType
	WHERE	EventTypeId = @EventTypeId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EventTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

