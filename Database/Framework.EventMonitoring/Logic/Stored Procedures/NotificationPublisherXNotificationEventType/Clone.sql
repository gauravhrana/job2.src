IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeClone')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeClone'
	DROP  Procedure NotificationPublisherXNotificationEventTypeClone
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeClone
(
		@NotificationPublisherXNotificationEventTypeId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@CreatedDateId								DECIMAL(15,0)				
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN		
	
	SELECT	@ApplicationId				= ApplicationId
		,	@NotificationEventTypeId		= NotificationEventTypeId
		,	@NotificationPublisherId		= NotificationPublisherId
		,	@CreatedDateId				=CreatedDateId				
	FROM	dbo.NotificationPublisherXNotificationEventType
	WHERE	NotificationPublisherXNotificationEventTypeId	= @NotificationPublisherXNotificationEventTypeId
	ORDER BY NotificationPublisherXNotificationEventTypeId

	EXEC dbo.NotificationPublisherXNotificationEventTypeInsert 
			@NotificationPublisherXNotificationEventTypeId		=	NULL
		,	@ApplicationId								=	@ApplicationId
		,	@NotificationEventTypeId						=	@NotificationEventTypeId
		,	@NotificationPublisherId							=	@NotificationPublisherId
		,	@AuditId									=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
