IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeInsert'
	DROP  Procedure NotificationEventTypeInsert
END
GO

PRINT 'Creating Procedure NotificationEventTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationEventTypeInsert
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

CREATE Procedure dbo.NotificationEventTypeInsert
(
		@NotificationEventTypeId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'NotificationEventType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationEventTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.NotificationEventType 
	( 
			NotificationEventTypeId
		,	ApplicationId	
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@NotificationEventTypeId
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationEventTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 