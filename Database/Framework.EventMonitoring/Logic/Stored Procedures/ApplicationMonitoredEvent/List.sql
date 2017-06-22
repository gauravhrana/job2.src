IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventList')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventList'
	DROP  Procedure  dbo.ApplicationMonitoredEventList
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventList
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
**     ----------					   ---------
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

CREATE Procedure dbo.ApplicationMonitoredEventList
(
		@AuditId				INT		
	,	@ApplicationId									INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEvent'
)
AS
BEGIN

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
	WHERE a.ApplicationId = @ApplicationId
	ORDER BY a.ApplicationMonitoredEventId	ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO