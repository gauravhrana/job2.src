IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceChildrenGet'
	DROP  Procedure ApplicationMonitoredEventSourceChildrenGet
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceChildrenGet
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceChildrenGet
(
		@ApplicationMonitoredEventSourceId		INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL   
	,	@SystemEntityType						VARCHAR(50) = 'ApplicationMonitoredEventSource'
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
	WHERE	a.ApplicationMonitoredEventSourceId = @ApplicationMonitoredEventSourceId
	
	-- GET ApplicationMonitoredEventSourceXProject Records
	SELECT	a.ApplicationMonitoredEventEmailId 
		,	a.ApplicationMonitoredEventSourceId
		,	a.UserId 
		,	a.CorrespondenceLevel
		,	a.Active
		,	b.Code								AS	'ApplicationMonitoredEventSource'
		,	c.FirstName + c.LastName			AS	'ApplicationUser'	
	FROM		dbo.ApplicationMonitoredEventEmail					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource					b		ON		a.ApplicationMonitoredEventSourceId		= b.ApplicationMonitoredEventSourceId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c		ON		a.UserId								= c.ApplicationUserId
	WHERE	a.ApplicationMonitoredEventSourceId = @ApplicationMonitoredEventSourceId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   