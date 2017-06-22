IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateInsert'
	DROP  Procedure ApplicationMonitoredEventProcessingStateInsert
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationMonitoredEventProcessingStateInsert
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

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateInsert
(
		@ApplicationMonitoredEventProcessingStateId		INT			= NULL 	OUTPUT	
	,	@ApplicationId									INT	
	,	@Code											VARCHAR(20)						
	,	@Description									VARCHAR(50)							
	,	@AuditId										INT									
	,	@AuditDate										DATETIME	= NULL				
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationMonitoredEventProcessingStateId OUTPUT, @AuditId
		
	INSERT INTO dbo.ApplicationMonitoredEventProcessingState 
	( 
			ApplicationMonitoredEventProcessingStateId	
		,	ApplicationId					
		,	Code											
		,	Description						
	)
	VALUES 
	(  
			@ApplicationMonitoredEventProcessingStateId		
		,	@ApplicationId
		,	@Code											
		,	@Description		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO

 