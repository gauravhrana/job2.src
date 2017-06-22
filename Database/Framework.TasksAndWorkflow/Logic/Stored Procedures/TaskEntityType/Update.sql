IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeUpdate'
	DROP  Procedure  TaskEntityTypeUpdate
END
GO

PRINT 'Creating Procedure TaskEntityTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityTypeUpdate
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

CREATE Procedure dbo.TaskEntityTypeUpdate
(
		@TaskEntityTypeId		INT			
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(50)			
	,	@Active					INT					
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'
)
AS
BEGIN 

	UPDATE	dbo.TaskEntityType 
	SET		Name				=	@Name				
		,	Description			=	@Description		
		,	@Active				=	@Active					
		,	SortOrder			=	@SortOrder							
	WHERE	TaskEntityTypeId	=	@TaskEntityTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO