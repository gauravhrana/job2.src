IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateChildrenGet'
	DROP  Procedure ApplicationMonitoredEventProcessingStateChildrenGet
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateChildrenGet
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateChildrenGet
(
		@ApplicationMonitoredEventProcessingStateId		INT					
	,	@AuditId										INT					
	,	@AuditDate										DATETIME	= NULL   
	,	@SystemEntityType								VARCHAR(50) = 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN

	-- GET ApplicationMonitoredEvent Records
	SELECT	a.ApplicationMonitoredEventId
		,	a.ApplicationMonitoredEventSourceId			
		,	a.ApplicationMonitoredEventProcessingStateId	
		,	a.ReferenceId	
		,	a.ApplicationId								
		,	a.ReferenceCode								
		,	a.Category	
		,	a.Message 		
		,	a.IsDuplicate		
		,	a.LastModifiedBy	
		,	a.LastModifiedOn											
		,	b.Code								AS	'ApplicationMonitoredEventSource'
		,	c.Code								AS	'ApplicationMonitoredEventProcessingState'
	FROM		dbo.ApplicationMonitoredEvent					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource				b		ON		a.ApplicationMonitoredEventSourceId				= b.ApplicationMonitoredEventSourceId
	INNER JOIN	dbo.ApplicationMonitoredEventProcessingState	c		ON		a.ApplicationMonitoredEventProcessingStateId	= c.ApplicationMonitoredEventProcessingStateId
	WHERE	a.ApplicationMonitoredEventProcessingStateId = @ApplicationMonitoredEventProcessingStateId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   