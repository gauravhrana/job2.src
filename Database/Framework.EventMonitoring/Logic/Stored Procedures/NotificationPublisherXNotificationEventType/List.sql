IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeList')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeList'
	DROP  Procedure  dbo.NotificationPublisherXNotificationEventTypeList
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeList
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
**     ----------					   ---------
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

CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationPublisherXNotificationEventType'
)
AS
BEGIN

	SELECT	a.NotificationPublisherXNotificationEventTypeId
		,	a.ApplicationId		
		,	a.NotificationEventTypeId					
		,	a.NotificationPublisherId
		,	a.CreatedDateId
		,	b.Name		AS	'NotificationEventType'			
		,	c.Name		AS	'NotificationPublisher'
	FROM		dbo.NotificationPublisherXNotificationEventType	a
	INNER JOIN	dbo.NotificationEventType			b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	dbo.NotificationPublisher					c	ON	a.NotificationPublisherId			=	c.NotificationPublisherId
	ORDER BY	a.NotificationPublisherXNotificationEventTypeId		ASC
		,		a.NotificationEventTypeId						ASC
		,		a.NotificationPublisherId								ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO