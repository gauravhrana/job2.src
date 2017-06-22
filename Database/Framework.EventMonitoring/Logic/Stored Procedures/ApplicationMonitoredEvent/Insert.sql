IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventInsert'
	DROP  Procedure ApplicationMonitoredEventInsert
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationMonitoredEventInsert
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

CREATE Procedure dbo.ApplicationMonitoredEventInsert
(
		@ApplicationMonitoredEventId					INT				= NULL 	OUTPUT		
	,	@ApplicationMonitoredEventSourceId				INT									
	,	@ApplicationMonitoredEventProcessingStateId		INT									
	,	@ApplicationId									INT
	,	@ReferenceId									INT									
	,	@ReferenceCode									VARCHAR(50)							
	,	@Category										VARCHAR(50)							
	,	@Message										VARCHAR(500)						
	,	@IsDuplicate									BIT									
	,	@LastModifiedBy									VARCHAR(50)							
	,	@LastModifiedOn									DATETIME							
	,	@AuditId										INT										
	,	@AuditDate										DATETIME		= NULL				
	,	@SystemEntityType								VARCHAR(50)		= 'ApplicationMonitoredEvent'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationMonitoredEventId OUTPUT, @AuditId
		
	INSERT INTO dbo.ApplicationMonitoredEvent 
	( 
			ApplicationMonitoredEventId 
		,	ApplicationMonitoredEventSourceId 
		,	ApplicationMonitoredEventProcessingStateId
		,	ReferenceId
		,	ReferenceCode
		,	ApplicationId
		,	Category
		,	Message 
		,	IsDuplicate
		,	LastModifiedBy
		,	LastModifiedOn
	)	
	VALUES 
	(  
			@ApplicationMonitoredEventId 
		,	@ApplicationMonitoredEventSourceId 
		,	@ApplicationMonitoredEventProcessingStateId
		,	@ReferenceId
		,	@ReferenceCode
		,	@ApplicationId
		,	@Category
		,	@Message 
		,	@IsDuplicate
		,	@LastModifiedBy
		,	@LastModifiedOn
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 