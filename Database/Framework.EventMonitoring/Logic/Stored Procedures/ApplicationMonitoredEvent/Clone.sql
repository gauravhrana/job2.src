IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventClone'
	DROP  Procedure ApplicationMonitoredEventClone
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationMonitoredEventClone
(
		@ApplicationMonitoredEventId					INT			= NULL 	OUTPUT		
	,	@ApplicationMonitoredEventSourceId				INT								
	,	@ApplicationMonitoredEventProcessingStateId		INT								
	,	@ReferenceId									INT								
	,	@ReferenceCode									VARCHAR(50)						
	,	@Category										VARCHAR(50)						
	,	@Message										VARCHAR(500)					
	,	@IsDuplicate									BIT								
	,	@LastModifiedBy									VARCHAR(50)						
	,	@LastModifiedOn									DATETIME						
	,	@AuditId										INT									
	,	@AuditDate										DATETIME	= NULL				
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEvent'
)
AS
BEGIN	
	
	SELECT	@ApplicationMonitoredEventSourceId 				=	ApplicationMonitoredEventSourceId			
		,	@ApplicationMonitoredEventProcessingStateId		=	ApplicationMonitoredEventProcessingStateId	
		,	@ReferenceId									=	ReferenceId									
		,	@ReferenceCode									=	ReferenceCode								
		,	@Category										=	Category									
		,	@Message 										=	Message 									
		,	@IsDuplicate									=	IsDuplicate									
		,	@LastModifiedBy									=	LastModifiedBy								
		,	@LastModifiedOn									=	LastModifiedOn								
	FROM	dbo.ApplicationMonitoredEvent
	WHERE	ApplicationMonitoredEventId			= @ApplicationMonitoredEventId

	EXEC dbo.ApplicationMonitoredEventInsert 
			@ApplicationMonitoredEventId					=	NULL
		,	@ApplicationMonitoredEventProcessingStateId		=	@ApplicationMonitoredEventProcessingStateId	
		,	@ReferenceId									=	@ReferenceId								
		,	@ReferenceCode									=	@ReferenceCode								
		,	@Category										=	@Category									
		,	@Message 										=	@Message 									
		,	@IsDuplicate									=	@IsDuplicate								
		,	@LastModifiedBy									=	@LastModifiedBy								
		,	@LastModifiedOn									=	@LastModifiedOn														
		,	@AuditId										=	@AuditId
			
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
