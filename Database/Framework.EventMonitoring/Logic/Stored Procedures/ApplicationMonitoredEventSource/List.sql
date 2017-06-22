IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceList')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceList'
	DROP  Procedure  dbo.ApplicationMonitoredEventSourceList
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceList
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceList
(
		@AuditId				INT					
	,	@ApplicationId			INT
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN

	SELECT	ApplicationMonitoredEventSourceId	
		,	ApplicationId
		,	Code								
		,	Description							
	FROM	 dbo.ApplicationMonitoredEventSource
	WHERE	ApplicationId = @ApplicationId
	ORDER BY ApplicationMonitoredEventSourceId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO