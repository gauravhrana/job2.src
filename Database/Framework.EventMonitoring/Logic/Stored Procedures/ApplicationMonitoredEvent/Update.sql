IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventUpdate'
	DROP  Procedure  ApplicationMonitoredEventUpdate
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventUpdate
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

CREATE Procedure dbo.ApplicationMonitoredEventUpdate
(
		@ApplicationMonitoredEventId					INT		 									
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

	UPDATE	dbo.ApplicationMonitoredEvent 
	SET		ApplicationMonitoredEventSourceId				=	@ApplicationMonitoredEventSourceId 
		,	ApplicationMonitoredEventProcessingStateId		=	@ApplicationMonitoredEventProcessingStateId
		,	ReferenceId										=	@ReferenceId
		,	ReferenceCode									=	@ReferenceCode
		,	Category										=	@Category
		,	Message 										=	@Message 
		,	IsDuplicate										=	@IsDuplicate
		,	LastModifiedBy									=	@LastModifiedBy
		,	LastModifiedOn									=	@LastModifiedOn
			
	WHERE	ApplicationMonitoredEventId	=	@ApplicationMonitoredEventId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO