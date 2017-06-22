IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateClone'
	DROP  Procedure ApplicationMonitoredEventProcessingStateClone
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateClone
(
		@ApplicationMonitoredEventProcessingStateId		INT			= NULL 	OUTPUT	
	,	@ApplicationId									INT			= NULL	
	,	@Code											VARCHAR(20)						
	,	@Description									VARCHAR(50)						
	,	@AuditId										INT									
	,	@AuditDate										DATETIME	= NULL				
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN			
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description			
	FROM	dbo.ApplicationMonitoredEventProcessingState
	WHERE	ApplicationMonitoredEventProcessingStateId			= @ApplicationMonitoredEventProcessingStateId

	EXEC dbo.ApplicationMonitoredEventProcessingStateInsert 
			@ApplicationMonitoredEventProcessingStateId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Code				=	@Code
		,	@Description		=	@Description
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
