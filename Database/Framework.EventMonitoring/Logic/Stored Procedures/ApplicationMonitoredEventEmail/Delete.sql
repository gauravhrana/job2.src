IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailDelete'
	DROP  Procedure ApplicationMonitoredEventEmailDelete
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventEmailDelete
(
		@ApplicationMonitoredEventEmailId 		INT						
	,	@AuditId								INT						
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN

	DELETE	 dbo.ApplicationMonitoredEventEmail
	WHERE	 ApplicationMonitoredEventEmailId = @ApplicationMonitoredEventEmailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
