IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceUpdate'
	DROP  Procedure  ApplicationMonitoredEventSourceUpdate
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceUpdate
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceUpdate
(
		@ApplicationMonitoredEventSourceId		INT	
	,	@Code									VARCHAR(20)					
	,	@Description							VARCHAR(50)				
	,	@AuditId								INT							
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationMonitoredEventSource 
	SET		Code			=	@Code				
		,	Description		=	@Description						
	WHERE	ApplicationMonitoredEventSourceId		=	@ApplicationMonitoredEventSourceId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO