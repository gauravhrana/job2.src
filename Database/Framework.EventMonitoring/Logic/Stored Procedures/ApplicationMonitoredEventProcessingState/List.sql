IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateList')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateList'
	DROP  Procedure  dbo.ApplicationMonitoredEventProcessingStateList
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateList
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

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateList
(
		@AuditId				INT		
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN

	SELECT	ApplicationMonitoredEventProcessingStateId	
		,	ApplicationId
		,	Code										
		,	Description							
	FROM	dbo.ApplicationMonitoredEventProcessingState
	WHERE ApplicationId = @ApplicationId
	ORDER BY ApplicationMonitoredEventProcessingStateId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO