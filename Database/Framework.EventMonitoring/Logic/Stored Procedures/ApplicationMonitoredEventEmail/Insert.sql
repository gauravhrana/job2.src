IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailInsert'
	DROP  Procedure ApplicationMonitoredEventEmailInsert
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationMonitoredEventEmailInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ApplicationMonitoredEventEmailInsert
(
		@ApplicationMonitoredEventEmailId		INT			= NULL 	OUTPUT		
	,	@ApplicationMonitoredEventSourceId		INT		
	,	@ApplicationId							INT
	,	@UserId									INT								
	,	@CorrespondenceLevel					VARCHAR(20)						
	,	@Active									BIT								
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationMonitoredEventEmailId OUTPUT, @AuditId
		
	INSERT INTO dbo.ApplicationMonitoredEventEmail 
	( 
			ApplicationMonitoredEventEmailId 
		,	ApplicationMonitoredEventSourceId 
		,	UserId 
		,	CorrespondenceLevel
		,	Active		
		,	ApplicationId				
	)	
	VALUES 
	(  
			@ApplicationMonitoredEventEmailId 
		,	@ApplicationMonitoredEventSourceId 
		,	@UserId 
		,	@CorrespondenceLevel
		,	@Active				
		,	@ApplicationId						
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 