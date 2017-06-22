IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceClone'
	DROP  Procedure ApplicationMonitoredEventSourceClone
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceClone
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceClone
(
		@ApplicationMonitoredEventSourceId		INT			= NULL 	OUTPUT	
	,	@ApplicationId							INT			= NULL
	,	@Code									VARCHAR(20)						
	,	@Description							VARCHAR(50)						
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description			
	FROM	dbo.ApplicationMonitoredEventSource
	WHERE	ApplicationMonitoredEventSourceId			= @ApplicationMonitoredEventSourceId

	EXEC dbo.ApplicationMonitoredEventSourceInsert 
			@ApplicationMonitoredEventSourceId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Code				=	@Code
		,	@Description		=	@Description
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
