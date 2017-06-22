IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskRunUpdate'
	DROP  Procedure  TaskRunUpdate
END
GO

PRINT 'Creating Procedure TaskRunUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskRunUpdate
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

CREATE Procedure dbo.TaskRunUpdate
(
		@TaskRunId				INT	 			
	,	@TaskScheduleId			INT					
	,	@TaskEntityId			INT					
	,	@BusinessDate			Varchar(30)								
	,	@StartTime				DATETIME			
	,	@EndTime				DATETIME			
	,	@RunBy					VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'	
)
AS
BEGIN 

	Declare @BusinessDateValue DATETIME = convert(DATETIME,@BusinessDate,120)

	DECLARE @BusinessDateId int=cast(CONVERT(varchar(20),@BusinessDateValue,112) as INT)


	UPDATE	dbo.TaskRun 
	SET		TaskScheduleId	=	@TaskScheduleId	
		,	TaskEntityId	=	@TaskEntityId	
		,	BusinessDate	=	@BusinessDate	
		,	StartTime		=	@StartTime		
		,	EndTime			=	@EndTime		
		,	RunBy			=	@RunBy								
	WHERE	TaskRunId		=	@TaskRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskRunId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO