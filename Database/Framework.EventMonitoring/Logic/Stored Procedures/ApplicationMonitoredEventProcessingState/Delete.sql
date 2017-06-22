IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateDelete'
	DROP  Procedure ApplicationMonitoredEventProcessingStateDelete
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateDelete
(
		@ApplicationMonitoredEventProcessingStateId 	INT						
	,	@AuditId										INT						
	,	@AuditDate										DATETIME	= NULL		
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN

	DELETE	 dbo.ApplicationMonitoredEventProcessingState
	WHERE	 ApplicationMonitoredEventProcessingStateId = @ApplicationMonitoredEventProcessingStateId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
