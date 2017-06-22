IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeInsert'
	DROP  Procedure NotificationPublisherXNotificationEventTypeInsert
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationPublisherXNotificationEventTypeInsert
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
**********************************************************************************************/

CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeInsert
(
		@NotificationPublisherXNotificationEventTypeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@NotificationEventTypeId						INT								
	,	@NotificationPublisherId							INT	
	,	@CreatedDateId								DECIMAL(15,0)							
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationPublisherXNotificationEventTypeId OUTPUT, @AuditId
	
	INSERT INTO dbo.NotificationPublisherXNotificationEventType 
	( 
			NotificationPublisherXNotificationEventTypeId		
		,	ApplicationId			
		,	NotificationEventTypeId					
		,	NotificationPublisherId
		,	CreatedDateId						
	)
	VALUES 
	(  
			@NotificationPublisherXNotificationEventTypeId		
		,	@ApplicationId			
		,	@NotificationEventTypeId			
		,	@NotificationPublisherId	
		,	@CreatedDateId								
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 