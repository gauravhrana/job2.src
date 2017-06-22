IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeUpdate'
	DROP  Procedure  NotificationPublisherXEventTypeUpdate
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherXEventTypeUpdate
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

CREATE Procedure dbo.NotificationPublisherXEventTypeUpdate
(
		@NotificationPublisherXEventTypeId		INT		
	,	@ApplicationId								INT
	,	@NotificationEventTypeId						INT					
	,	@NotificationPublisherId							INT
   	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN

	DECLARE @Date DATETIME = Getdate()
	DECLARE @CreatedDateId INT = CONVERT(VARCHAR(30), @Date, 112)
	DECLARE @CreatedTimeId INT = Replace(convert(varchar(5),@Date,108),':','')+'00'
	

	UPDATE	dbo.NotificationPublisherXEventType 
	SET		NotificationEventTypeId		=	@NotificationEventTypeId		
		,	NotificationPublisherId			=	@NotificationPublisherId
		,	CreatedDateId					=	@CreatedDateId					
		,	CreatedTimeId					=	@CreatedTimeId									
	WHERE	NotificationPublisherXEventTypeId		=	@NotificationPublisherXEventTypeId
	AND		ApplicationId						=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

