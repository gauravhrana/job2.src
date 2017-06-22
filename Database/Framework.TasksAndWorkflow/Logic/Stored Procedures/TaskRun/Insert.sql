IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunInsert')
BEGIN
	PRINT 'Dropping Procedure TaskRunInsert'
	DROP  Procedure TaskRunInsert
END
GO

PRINT 'Creating Procedure TaskRunInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskRunInsert
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
CREATE Procedure dbo.TaskRunInsert
(
		@TaskRunId				INT				= NULL 	OUTPUT	
	,	@ApplicationId			INT
	,	@TaskScheduleId			INT								
	,	@TaskEntityId			INT								
	,	@BusinessDate			Varchar(30)								
	,	@StartTime				DATETIME						
	,	@EndTime				DATETIME						
	,	@RunBy					VARCHAR(50)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'TaskRun'
)
AS
BEGIN

		Declare @BusinessDateValue DATETIME = convert(DATETIME,@BusinessDate,120)

		DECLARE @BusinessDateId int=cast(CONVERT(varchar(20),@BusinessDateValue,112) as INT)


	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskRunId OUTPUT, @AuditId
		
	INSERT INTO dbo.TaskRun 
	( 
			TaskRunId	
		,	ApplicationId	
		,	TaskScheduleId	
		,	TaskEntityId	
		,	BusinessDate	
		,	StartTime		
		,	EndTime		
		,	RunBy					
	)
	VALUES 
	(  
			@TaskRunId	
		,	@ApplicationId	
		,	@TaskScheduleId	
		,	@TaskEntityId	
		,	@BusinessDate	
		,	@StartTime		
		,	@EndTime		
		,	@RunBy			
	)

	SELECT @TaskRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskRunId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 