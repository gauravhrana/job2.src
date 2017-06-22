IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeUpdate'
	DROP  Procedure  TaskScheduleTypeUpdate
END
GO

PRINT 'Creating Procedure TaskScheduleTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeUpdate
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

CREATE Procedure dbo.TaskScheduleTypeUpdate
(
		@TaskScheduleTypeId		INT					
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

	UPDATE	dbo.TaskScheduleType 
	SET		Name				=	@Name				
		,	Description			=	@Description		
		,	Active				=	@Active					
		,	SortOrder			=	@SortOrder							
	WHERE	TaskScheduleTypeId	=	@TaskScheduleTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO