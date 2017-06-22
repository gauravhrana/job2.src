IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeDelete'
	DROP  Procedure NotificationPublisherXNotificationEventTypeDelete
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeDelete
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
CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeDelete
(
		@NotificationPublisherXNotificationEventTypeId		INT			= NULL	
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN

	DELETE	dbo.NotificationPublisherXNotificationEventType
	WHERE	NotificationPublisherXNotificationEventTypeId	=	ISNULL(@NotificationPublisherXNotificationEventTypeId,	NotificationPublisherXNotificationEventTypeId)	
	AND		NotificationEventTypeId					=	ISNULL(@NotificationEventTypeId,			NotificationEventTypeId)
	AND		NotificationPublisherId						=	ISNULL(@NotificationPublisherId,			NotificationPublisherId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
