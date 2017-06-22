IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeDelete'
	DROP  Procedure NotificationPublisherXEventTypeDelete
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeDelete
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
CREATE Procedure dbo.NotificationPublisherXEventTypeDelete
(
		@NotificationPublisherXEventTypeId		INT			= NULL	
	,	@NotificationEventTypeId						INT			= NULL	
	,	@NotificationPublisherId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN

	DELETE	dbo.NotificationPublisherXEventType
	WHERE	NotificationPublisherXEventTypeId	=	ISNULL(@NotificationPublisherXEventTypeId,	NotificationPublisherXEventTypeId)	
	AND		NotificationEventTypeId					=	ISNULL(@NotificationEventTypeId,			NotificationEventTypeId)
	AND		NotificationPublisherId						=	ISNULL(@NotificationPublisherId,			NotificationPublisherId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
