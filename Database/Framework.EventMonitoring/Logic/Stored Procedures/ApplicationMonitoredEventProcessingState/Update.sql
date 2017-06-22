IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateUpdate'
	DROP  Procedure  ApplicationMonitoredEventProcessingStateUpdate
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateUpdate
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

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateUpdate
(
		@ApplicationMonitoredEventProcessingStateId		INT						
	,	@Code											VARCHAR(20)					
	,	@Description									VARCHAR(50)				
	,	@AuditId										INT							
	,	@AuditDate										DATETIME	= NULL		
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN 


	UPDATE	dbo.ApplicationMonitoredEventProcessingState 
	SET		Code			=	@Code				
		,	Description		=	@Description						
	WHERE	ApplicationMonitoredEventProcessingStateId		=	@ApplicationMonitoredEventProcessingStateId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO