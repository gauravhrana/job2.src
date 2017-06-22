IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeDetails')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeDetails'
	DROP  Procedure NotificationPublisherXEventTypeDetails
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeDetails
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

CREATE Procedure dbo.NotificationPublisherXEventTypeDetails
(
		@NotificationPublisherXEventTypeId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NotificationPublisherXEventTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.NotificationPublisherXEventTypeId	
		,	a.ApplicationId	
		,	a.NotificationEventTypeId						
		,	a.NotificationPublisherId	
		,	a.CreatedDateId				
		,	a.CreatedTimeId																				
		,	b.Name				AS	'NotificationEventType'			
		,	c.Name				AS	'NotificationPublisher'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.NotificationPublisherXEventType	a
	INNER JOIN	dbo.NotificationEventType			b	ON	a.NotificationEventTypeId	=	b.NotificationEventTypeId
	INNER JOIN	dbo.NotificationPublisher					c	ON	a.NotificationPublisherId	=	c.NotificationPublisherId
	WHERE	a.NotificationPublisherXEventTypeId	=	ISNULL(@NotificationPublisherXEventTypeId,	a.NotificationPublisherXEventTypeId)	
	AND		a.NotificationEventTypeId					=	ISNULL(@NotificationEventTypeId,			a.NotificationEventTypeId)
	AND		a.NotificationPublisherId							=	ISNULL(@NotificationPublisherId,			a.NotificationPublisherId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   