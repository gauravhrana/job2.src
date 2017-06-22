IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleInsert')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleInsert'
	DROP  Procedure TaskScheduleInsert
END
GO

PRINT 'Creating Procedure TaskScheduleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskScheduleInsert
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

CREATE Procedure dbo.TaskScheduleInsert
(
		@TaskScheduleId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@TaskScheduleTypeId		INT								
	,	@TaskEntityId			INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskScheduleId OUTPUT, @AuditId
		
	SET IDENTITY_INSERT TaskSchedule ON

	INSERT INTO dbo.TaskSchedule 
	( 
			TaskScheduleId	
		,	ApplicationId	
		,	TaskScheduleTypeId	
		,	TaskEntityId			
	)
	VALUES 
	(  
			@TaskScheduleId	
		,	@ApplicationId	
		,	@TaskScheduleTypeId	
		,	@TaskEntityId		
	)

	SET IDENTITY_INSERT TaskSchedule OFF

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 