IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeList')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeList'
	DROP  Procedure  dbo.NotificationPublisherXEventTypeList
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeList
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

CREATE Procedure dbo.NotificationPublisherXEventTypeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN

	SELECT	a.NotificationPublisherXEventTypeId
		,	a.ApplicationId		
		,	a.NotificationEventTypeId					
		,	a.NotificationPublisherId
		,	a.CreatedDateId
		,	a.CreatedTimeId
		,	b.Name		AS	'NotificationEventType'			
		,	c.Name		AS	'NotificationPublisher'
	FROM		dbo.NotificationPublisherXEventType	a
	INNER JOIN	dbo.NotificationEventType			b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	dbo.NotificationPublisher					c	ON	a.NotificationPublisherId			=	c.NotificationPublisherId
	ORDER BY	a.NotificationPublisherXEventTypeId		ASC
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