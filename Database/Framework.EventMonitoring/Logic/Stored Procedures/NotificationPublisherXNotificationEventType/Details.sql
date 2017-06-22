IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeDetails')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeDetails'
	DROP  Procedure NotificationPublisherXNotificationEventTypeDetails
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeDetails
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

CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeDetails
(
		@NotificationPublisherXNotificationEventTypeId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NotificationPublisherXNotificationEventTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.NotificationPublisherXNotificationEventTypeId	
		,	a.ApplicationId	
		,	a.NotificationEventTypeId						
		,	a.NotificationPublisherId	
		,	a.CreatedDateId												
		,	b.Name				AS	'NotificationEventType'			
		,	c.Name				AS	'NotificationPublisher'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.NotificationPublisherXNotificationEventType	a
	INNER JOIN	dbo.NotificationEventType			b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	dbo.NotificationPublisher					c	ON	a.NotificationPublisherId	=	c.NotificationPublisherId
	WHERE	a.NotificationPublisherXNotificationEventTypeId	=	ISNULL(@NotificationPublisherXNotificationEventTypeId,	a.NotificationPublisherXNotificationEventTypeId)	
	AND		a.NotificationEventTypeId					=	ISNULL(@NotificationEventTypeId,			a.NotificationEventTypeId)
	AND		a.NotificationPublisherId							=	ISNULL(@NotificationPublisherId,			a.NotificationPublisherId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   