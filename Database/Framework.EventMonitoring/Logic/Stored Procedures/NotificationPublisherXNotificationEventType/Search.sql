IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='NotificationPublisherXNotificationEventTypeSearch')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeSearch'
	DROP Procedure NotificationPublisherXNotificationEventTypeSearch
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXNotificationEventTypeSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC NotificationPublisherXNotificationEventTypeSearch NULL	, NULL	, NULL
			EXEC NotificationPublisherXNotificationEventTypeSearch NULL	, 'K'	, NULL
			EXEC NotificationPublisherXNotificationEventTypeSearch 1	, 'K'	, NULL
			EXEC NotificationPublisherXNotificationEventTypeSearch 1	, NULL	, NULL
			EXEC NotificationPublisherXNotificationEventTypeSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure NotificationPublisherXNotificationEventTypeSearch
(
		@NotificationPublisherXNotificationEventTypeId		INT				= NULL	
	,	@NotificationEventTypeId							INT				= NULL	
	,	@NotificationPublisherId							INT				= NULL
	,	@CreatedDateId										DECIMAL(15)		=	NULL	
	,	@ApplicationId										INT				= NULL
	,	@AuditId											INT						
	,	@AuditDate											DATETIME		= NULL
	,	@SystemEntityType									VARCHAR(50)		= 'NotificationPublisherXNotificationEventType' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	
	SELECT	a.NotificationPublisherXNotificationEventTypeId	
		,	a.ApplicationId	
		,	a.NotificationEventTypeId						
		,	a.NotificationPublisherId
		,	a.CreatedDateId								
		,	b.Name		AS	'NotificationEventType'			
		,	c.Name		AS	'NotificationPublisher'
	FROM		dbo.NotificationPublisherXNotificationEventType	a
	INNER JOIN	NotificationEventType				b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	NotificationPublisher						c	ON	a.NotificationPublisherId	=	c.NotificationPublisherId
	WHERE   a.NotificationPublisherXNotificationEventTypeId = ISNULL(@NotificationPublisherXNotificationEventTypeId, a.NotificationPublisherXNotificationEventTypeId )
	AND		a.NotificationEventTypeId	= ISNULL(@NotificationEventTypeId, a.NotificationEventTypeId )
	AND		a.NotificationPublisherId	= ISNULL(@NotificationPublisherId, a.NotificationPublisherId )
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.CreatedDateId				= ISNULL(@CreatedDateId, a.CreatedDateId)
	ORDER BY a.NotificationPublisherXNotificationEventTypeId	ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXNotificationEventTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

