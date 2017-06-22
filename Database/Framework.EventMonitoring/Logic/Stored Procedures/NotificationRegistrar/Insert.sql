IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarInsert')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarInsert'
	DROP  Procedure NotificationRegistrarInsert
END
GO

PRINT 'Creating Procedure NotificationRegistrarInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:NotificationRegistrarInsert
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
**		Date:		Author:				Developer:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.NotificationRegistrarInsert
(
		@NotificationRegistrarId				INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT
	,	@NotificationEventTypeId					INT
	,	@NotificationPublisherId					INT				= NULL
	,	@Message					VARCHAR(255)	
  	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'NotificationRegistrar'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationRegistrarId OUTPUT, @AuditId
    
    DECLARE @Date DATETIME = Getdate()
	DECLARE @PublishDateId INT = CONVERT(VARCHAR(30), @Date, 112)
	DECLARE @PublishTimeId INT = Replace(convert(varchar(5),@Date,108),':','')+'00'
	
	INSERT INTO dbo.NotificationRegistrar 
	( 
			NotificationRegistrarId	
		,   ApplicationId					
		,	NotificationEventTypeId
		,	NotificationPublisherId
		,	Message
		,	PublishDateId		
		,	PublishTimeId						
	
	)
	VALUES 
	(  
			@NotificationRegistrarId	
		,   @ApplicationId	
		,	@NotificationEventTypeId
		,	@NotificationPublisherId	
		,	@Message
		,	@PublishDateId		
		,	@PublishTimeId				
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
