IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberInsert'
	DROP  Procedure NotificationSubscriberInsert
END
GO

PRINT 'Creating Procedure NotificationSubscriberInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationSubscriberInsert
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

CREATE Procedure dbo.NotificationSubscriberInsert
(
		@NotificationSubscriberId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'NotificationSubscriber'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationSubscriberId OUTPUT, @AuditId
		
	INSERT INTO dbo.NotificationSubscriber 
	( 
			NotificationSubscriberId
		,	ApplicationId	
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@NotificationSubscriberId
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationSubscriberId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 