IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunClone')
BEGIN
	PRINT 'Dropping Procedure TaskRunClone'
	DROP  Procedure TaskRunClone
END
GO

PRINT 'Creating Procedure TaskRunClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskRunClone
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

CREATE Procedure dbo.TaskRunClone
(
		@TaskRunId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			= NULL	
	,	@TaskScheduleId			INT								
	,	@TaskEntityId			INT								
	,	@BusinessDate			INT								
	,	@StartTime				DATETIME						
	,	@EndTime				DATETIME						
	,	@RunBy					VARCHAR(50)							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'			
)
AS
BEGIN		

	SELECT	@ApplicationId		=	ISNULL(@ApplicationId, ApplicationId)										
		,	@TaskScheduleId		=	ISNULL(@TaskScheduleId, TaskScheduleId)										
		,	@TaskEntityId		=	ISNULL(@TaskEntityId, TaskEntityId)									
		,	@BusinessDate		=	ISNULL(@BusinessDate, BusinessDate)									
		,	@StartTime			=	ISNULL(@StartTime, StartTime)											
		,	@EndTime			=	ISNULL(@EndTime, EndTime)												
		,	@RunBy				=	ISNULL(@RunBy, RunBy)
	FROM	dbo.TaskRun
	WHERE	TaskRunId			=	@TaskRunId

	EXEC	dbo.TaskRunInsert 
			@TaskRunId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@TaskScheduleId		=	@TaskScheduleId	
		,	@TaskEntityId		=	@TaskEntityId	
		,	@BusinessDate		=	@BusinessDate	
		,	@StartTime			=	@StartTime		
		,	@EndTime			=	@EndTime			
		,	@RunBy				=	@RunBy			
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType	=	@SystemEntityType
		,	@EntityKey			=	@TaskRunId
		,	@AuditAction		=	'Clone'
		,	@CreatedDate		=	@AuditDate
		,	@CreatedByPersonId	=	@AuditId	

END	
GO
