IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherUpdate'
	DROP  Procedure  NotificationPublisherUpdate
END
GO

PRINT 'Creating Procedure NotificationPublisherUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherUpdate
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

CREATE Procedure dbo.NotificationPublisherUpdate
(
		@NotificationPublisherId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)	
	,	@ApplicationId			INT					
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'NotificationPublisher'
)
AS
BEGIN 

	UPDATE	dbo.NotificationPublisher 
	SET		Name				=	@Name	
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	NotificationPublisherId			=	@NotificationPublisherId
	AND 	ApplicationId		=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationPublisherId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO