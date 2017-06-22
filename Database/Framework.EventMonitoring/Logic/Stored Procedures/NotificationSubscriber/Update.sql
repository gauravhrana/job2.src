IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberUpdate'
	DROP  Procedure  NotificationSubscriberUpdate
END
GO

PRINT 'Creating Procedure NotificationSubscriberUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationSubscriberUpdate
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

CREATE Procedure dbo.NotificationSubscriberUpdate
(
		@NotificationSubscriberId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)	
	,	@ApplicationId			INT					
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'NotificationSubscriber'
)
AS
BEGIN 

	UPDATE	dbo.NotificationSubscriber 
	SET		Name				=	@Name	
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	NotificationSubscriberId			=	@NotificationSubscriberId
	AND 	ApplicationId		=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationSubscriberId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO