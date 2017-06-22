IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventDoesExist'
	DROP  Procedure  ApplicationMonitoredEventDoesExist
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.ApplicationMonitoredEventDoesExist
(
		@ApplicationMonitoredEventSourceId				INT							
	,	@ApplicationMonitoredEventProcessingStateId		INT							
	,	@ReferenceId									INT							
	,	@Category										VARCHAR(50)					
	,	@Message										VARCHAR(500)				
	,	@AuditId										INT							
	,	@AuditDate										DATETIME		= NULL		
	,	@SystemEntityType								VARCHAR(50)		= 'ApplicatinMonitoredEvent'			
)	
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ApplicationMonitoredEvent a
	WHERE		a.ApplicationMonitoredEventSourceId				=	@ApplicationMonitoredEventSourceId
	AND			a.ApplicationMonitoredEventProcessingStateId	=	@ApplicationMonitoredEventProcessingStateId
	AND			a.ReferenceId									=	@ReferenceId
	AND			a.Category										=	@Category
	AND			a.Message										=	@Message

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

