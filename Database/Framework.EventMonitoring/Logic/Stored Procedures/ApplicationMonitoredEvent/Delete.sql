IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventDelete'
	DROP  Procedure ApplicationMonitoredEventDelete
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventDelete
(
		@ApplicationMonitoredEventId 		INT						
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL		
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationMonitoredEvent'
)
AS
BEGIN

	DELETE	 dbo.ApplicationMonitoredEvent
	WHERE	 ApplicationMonitoredEventId = @ApplicationMonitoredEventId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
