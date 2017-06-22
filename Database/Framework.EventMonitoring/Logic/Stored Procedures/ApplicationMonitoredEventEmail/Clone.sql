IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailClone'
	DROP  Procedure ApplicationMonitoredEventEmailClone
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailClone
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

CREATE Procedure dbo.ApplicationMonitoredEventEmailClone
(
		@ApplicationMonitoredEventEmailId		INT			= NULL 	OUTPUT		
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

	SELECT	@ApplicationMonitoredEventEmailId	=	ApplicationMonitoredEventSourceId
		,	@UserId 							=	UserId 								
		,	@CorrespondenceLevel				=	CorrespondenceLevel					
		,	@Active								=	Active
	FROM	dbo.ApplicationMonitoredEventEmail
	WHERE	ApplicationMonitoredEventEmailId			= @ApplicationMonitoredEventEmailId

	EXEC dbo.ApplicationMonitoredEventEmailInsert 
			@ApplicationMonitoredEventEmailId	=	NULL
		,	@ApplicationMonitoredEventEmailId	=	@ApplicationMonitoredEventSourceId
		,	@UserId 							=	@UserId 							
		,	@CorrespondenceLevel				=	@CorrespondenceLevel				
		,	@Active								=	@Active							
		,	@AuditId							=	@AuditId
			
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
