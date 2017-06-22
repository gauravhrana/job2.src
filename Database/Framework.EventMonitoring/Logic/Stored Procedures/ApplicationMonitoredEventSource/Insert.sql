IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceInsert'
	DROP  Procedure ApplicationMonitoredEventSourceInsert
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationMonitoredEventSourceInsert
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceInsert
(
		@ApplicationMonitoredEventSourceId		INT			= NULL 	OUTPUT		
	,	@ApplicationId							INT
	,	@Code									VARCHAR(20)						
	,	@Description							VARCHAR(50)							
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationMonitoredEventSourceId OUTPUT, @AuditId
		
	INSERT INTO dbo.ApplicationMonitoredEventSource 
	( 
			ApplicationMonitoredEventSourceId	
		,	ApplicationId					
		,	Code									
		,	Description						
	)
	VALUES 
	(  
			@ApplicationMonitoredEventSourceId	
		,	@ApplicationId	
		,	@Code									
		,	@Description		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 