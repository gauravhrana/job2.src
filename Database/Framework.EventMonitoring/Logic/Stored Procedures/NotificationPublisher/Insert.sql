IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherInsert'
	DROP  Procedure NotificationPublisherInsert
END
GO

PRINT 'Creating Procedure NotificationPublisherInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationPublisherInsert
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

CREATE Procedure dbo.NotificationPublisherInsert
(
		@NotificationPublisherId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'NotificationPublisher'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationPublisherId OUTPUT, @AuditId
		
	INSERT INTO dbo.NotificationPublisher 
	( 
			NotificationPublisherId
		,	ApplicationId	
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@NotificationPublisherId
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 