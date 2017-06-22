IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskNotesDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskNotesDoesExist'
	DROP  Procedure  TaskNotesDoesExist
END
GO

PRINT 'Creating Procedure TaskNotesDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskNotesDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TaskNotesDoesExist
(
		@TaskNotesId			INT							
	,	@AuditId				INT							
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskNotes'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskNotes a
	WHERE		a.TaskNotesId			=	@TaskNotesId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

