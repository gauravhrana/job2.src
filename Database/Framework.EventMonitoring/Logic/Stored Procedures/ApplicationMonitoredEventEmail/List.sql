IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailList')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailList'
	DROP  Procedure  dbo.ApplicationMonitoredEventEmailList
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailList
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

CREATE Procedure dbo.ApplicationMonitoredEventEmailList
(
		@AuditId				INT		
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN

	SELECT	a.ApplicationMonitoredEventEmailId 
		,	a.ApplicationMonitoredEventSourceId
		,	a.UserId 
		,	a.CorrespondenceLevel
		,	a.Active
		,	b.Code								AS	'ApplicationMonitoredEventSource'
		,	c.FirstName + c.LastName			AS	'User'	
	FROM		dbo.ApplicationMonitoredEventEmail					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource					b		ON		a.ApplicationMonitoredEventSourceId		= b.ApplicationMonitoredEventSourceId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c		ON		a.UserId								= c.ApplicationUserId	
	WHERE a.ApplicationId = @ApplicationId
	ORDER BY a.ApplicationMonitoredEventEmailId	ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO