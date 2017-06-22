IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeClone')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeClone'
	DROP  Procedure NotificationPublisherXEventTypeClone
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeClone
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

CREATE Procedure dbo.NotificationPublisherXEventTypeClone
(
		@NotificationPublisherXEventTypeId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@CreatedDateId									INT			= NULL	
	,	@CreatedTimeId									INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN		
	
	SELECT	@ApplicationId				= ApplicationId
		,	@NotificationEventTypeId		= NotificationEventTypeId
		,	@NotificationPublisherId		= NotificationPublisherId
		,	@CreatedDateId				=CreatedDateId			
		,	@CreatedTimeId				=CreatedTimeId					
	FROM	dbo.NotificationPublisherXEventType
	WHERE	NotificationPublisherXEventTypeId	= @NotificationPublisherXEventTypeId
	ORDER BY NotificationPublisherXEventTypeId

	EXEC dbo.NotificationPublisherXEventTypeInsert 
			@NotificationPublisherXEventTypeId		=	NULL
		,	@ApplicationId								=	@ApplicationId
		,	@NotificationEventTypeId					=	@NotificationEventTypeId
		,	@NotificationPublisherId							=	@NotificationPublisherId
		,	@AuditId									=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
