IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='NotificationPublisherXEventTypeSearch')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeSearch'
	DROP Procedure NotificationPublisherXEventTypeSearch
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeSearch
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
			EXEC NotificationPublisherXEventTypeSearch NULL	, NULL	, NULL
			EXEC NotificationPublisherXEventTypeSearch NULL	, 'K'	, NULL
			EXEC NotificationPublisherXEventTypeSearch 1	, 'K'	, NULL
			EXEC NotificationPublisherXEventTypeSearch 1	, NULL	, NULL
			EXEC NotificationPublisherXEventTypeSearch NULL	, NULL	, 'W'

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
Create procedure NotificationPublisherXEventTypeSearch
(
		@NotificationPublisherXEventTypeId		INT				= NULL	
	,	@NotificationEventTypeId				INT				= NULL	
	,	@NotificationPublisherId				INT				= NULL
	,	@CreatedDateId							INT				=	NULL
	,	@CreatedTimeId							INT				=	NULL		
	,	@ApplicationId							INT				= NULL
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'NotificationPublisherXEventType' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON		
	SELECT	a.NotificationPublisherXEventTypeId	
		,	a.ApplicationId	
		,	a.NotificationEventTypeId						
		,	a.NotificationPublisherId
		,	a.CreatedDateId				
		,	a.CreatedTimeId												
		,	b.Name		AS	'NotificationEventType'			
		,	c.Name		AS	'NotificationPublisher'
	FROM		dbo.NotificationPublisherXEventType	a
	INNER JOIN	NotificationEventType				b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	NotificationPublisher						c	ON	a.NotificationPublisherId	=	c.NotificationPublisherId
	WHERE   a.NotificationPublisherXEventTypeId = ISNULL(@NotificationPublisherXEventTypeId, a.NotificationPublisherXEventTypeId )
	AND		a.NotificationEventTypeId	= ISNULL(@NotificationEventTypeId, a.NotificationEventTypeId )
	AND		a.NotificationPublisherId	= ISNULL(@NotificationPublisherId, a.NotificationPublisherId )
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.CreatedDateId				= ISNULL(@CreatedDateId, a.CreatedDateId)
	AND		a.CreatedTimeId				= ISNULL(@CreatedTimeId, a.CreatedTimeId)
	ORDER BY a.NotificationPublisherXEventTypeId	ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

