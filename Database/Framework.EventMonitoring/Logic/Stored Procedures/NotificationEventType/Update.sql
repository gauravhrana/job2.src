IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeUpdate'
	DROP  Procedure  NotificationEventTypeUpdate
END
GO

PRINT 'Creating Procedure NotificationEventTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationEventTypeUpdate
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

CREATE Procedure dbo.NotificationEventTypeUpdate
(
		@NotificationEventTypeId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)	
	,	@ApplicationId			INT					
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'NotificationEventType'
)
AS
BEGIN 

	UPDATE	dbo.NotificationEventType 
	SET		Name				=	@Name	
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	NotificationEventTypeId			=	@NotificationEventTypeId
	AND 	ApplicationId		=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationEventTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO