IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeInsert')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeInsert'
	DROP  Procedure TaskScheduleTypeInsert
END
GO

PRINT 'Creating Procedure TaskScheduleTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskScheduleTypeInsert
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

CREATE Procedure dbo.TaskScheduleTypeInsert
(
		@TaskScheduleTypeId		INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@Active					INT								
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskScheduleTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.TaskScheduleType 
	( 
			TaskScheduleTypeId			
		,	ApplicationId		
		,	Name				
		,	Description			
		,	Active				
		,	SortOrder						
	)
	VALUES 
	(  
			@TaskScheduleTypeId	
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@Active				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 