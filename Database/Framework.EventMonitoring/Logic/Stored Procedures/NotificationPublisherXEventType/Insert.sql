IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeInsert'
	DROP  Procedure NotificationPublisherXEventTypeInsert
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationPublisherXEventTypeInsert
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

CREATE Procedure dbo.NotificationPublisherXEventTypeInsert
(
		@NotificationPublisherXEventTypeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@NotificationEventTypeId						INT								
	,	@NotificationPublisherId							INT	
   	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'NotificationPublisherXEventType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationPublisherXEventTypeId OUTPUT, @AuditId
 
	DECLARE @Date DATETIME = Getdate()
	DECLARE @CreatedDateId INT = CONVERT(VARCHAR(30), @Date, 112)
	DECLARE @CreatedTimeId INT = Replace(convert(varchar(5),@Date,108),':','')+'00'
	
	
	INSERT INTO dbo.NotificationPublisherXEventType 
	( 
			NotificationPublisherXEventTypeId		
		,	ApplicationId			
		,	NotificationEventTypeId					
		,	NotificationPublisherId
		,	CreatedDateId
		,	CreatedTimeId						
	)
	VALUES 
	(  
			@NotificationPublisherXEventTypeId		
		,	@ApplicationId			
		,	@NotificationEventTypeId			
		,	@NotificationPublisherId	
		,	@CreatedDateId		
		,	@CreatedTimeId														
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherXEventTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO



 