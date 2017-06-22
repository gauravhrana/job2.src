IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailUpdate'
	DROP  Procedure  ApplicationMonitoredEventEmailUpdate
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailUpdate
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

CREATE Procedure dbo.ApplicationMonitoredEventEmailUpdate
(
		@ApplicationMonitoredEventEmailId		INT		 				
	,	@ApplicationMonitoredEventSourceId		INT						
	,	@UserId									INT						
	,	@CorrespondenceLevel					VARCHAR(20)				
	,	@Active									BIT						
	,	@AuditId								INT							
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationMonitoredEventEmail 
	SET		ApplicationMonitoredEventSourceId	=	@ApplicationMonitoredEventSourceId 
		,	UserId 								=	@UserId 
		,	CorrespondenceLevel					=	@CorrespondenceLevel
		,	Active								=	@Active											
	WHERE	ApplicationMonitoredEventEmailId	=	@ApplicationMonitoredEventEmailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO