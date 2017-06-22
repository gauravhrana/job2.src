IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentDeleteHard')
BEGIN
	PRINT 'Dropping Procedure CommentDeleteHard'
	DROP  Procedure CommentDeleteHard
END
GO

PRINT 'Creating Procedure CommentDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: CommentDelete
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

CREATE Procedure dbo.CommentDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Comment'	
)
AS
BEGIN

	IF @KeyType = 'CommentId'
		BEGIN

			DELETE	 dbo.Comment
			WHERE	 CommentId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Comment
			WHERE	 StudentId = @KeyId

		END
	ELSE IF @KeyType = 'EventTypeId'
		BEGIN

			DELETE	 dbo.Comment
			WHERE	 EventTypeId = @KeyId

		END 	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
