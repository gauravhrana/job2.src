IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskNotesDeleteHard'
	DROP  Procedure TaskNotesDeleteHard
END
GO

PRINT 'Creating Procedure TaskNotesDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TaskNotesDelete
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
CREATE Procedure dbo.TaskNotesDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskNotes'
)
AS
BEGIN

	IF @KeyType = 'TaskNotesId'
	BEGIN

		EXEC	dbo.TaskNotesDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TaskNotesId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TaskNotes
		WHERE	 TaskNotesId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
