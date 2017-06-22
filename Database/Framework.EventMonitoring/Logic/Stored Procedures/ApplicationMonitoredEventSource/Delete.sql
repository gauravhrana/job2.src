IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceDelete'
	DROP  Procedure ApplicationMonitoredEventSourceDelete
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventSourceDelete
(
		@ApplicationMonitoredEventSourceId 		INT						
	,	@AuditId								INT						
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN

	DELETE	 dbo.ApplicationMonitoredEventSource
	WHERE	 ApplicationMonitoredEventSourceId = @ApplicationMonitoredEventSourceId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
