IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunDelete')
BEGIN
	PRINT 'Dropping Procedure TaskRunDelete'
	DROP  Procedure TaskRunDelete
END
GO

PRINT 'Creating Procedure TaskRunDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskRunDelete
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
CREATE Procedure dbo.TaskRunDelete
(
		@TaskRunId 				INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'	
)
AS
BEGIN

	DELETE	 dbo.TaskRun
	WHERE	 TaskRunId = @TaskRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType	=	@SystemEntityType
		,	@EntityKey			=	@TaskRunId
		,	@AuditAction		=	'Delete'
		,	@CreatedDate		=	@AuditDate
		,	@CreatedByPersonId	=	@AuditId

END
GO
