IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeUpdate'
	DROP  Procedure  NotificationPublisherXNotificationEventTypeUpdate
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeUpdate
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

CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeUpdate
(
		@NotificationPublisherXNotificationEventTypeId		INT		
	,	@ApplicationId								INT
	,	@NotificationEventTypeId						INT					
	,	@NotificationPublisherId							INT
	,	@CreatedDateId							DECIMAL(15,0)							
	,	@AuditId									INT	
				
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN 

	UPDATE	dbo.NotificationPublisherXNotificationEventType 
	SET		NotificationEventTypeId		=	@NotificationEventTypeId		
		,	NotificationPublisherId			=	@NotificationPublisherId
		,	CreatedDateId					=	@CreatedDateId							
	WHERE	NotificationPublisherXNotificationEventTypeId		=	@NotificationPublisherXNotificationEventTypeId
	AND		ApplicationId						=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO