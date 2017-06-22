IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='CommentDoesExist')
BEGIN
	PRINT 'Dropping Procedure CommentDoesExist'
	DROP  Procedure  CommentDoesExist
END
GO

PRINT 'Creating Procedure CommentDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: CommentDoesExist
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

Create procedure dbo.CommentDoesExist
(
		@StudentId				INT				= NULL	
	,	@ApplicationId			INT			
	,	@Date					DATETIME		= NULL		
	,	@EventTypeId			INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Comment'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Comment a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.Date				=	@Date
	AND			a.EventTypeId		=	@EventTypeId
	AND			a.ApplicationId		=	@ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

