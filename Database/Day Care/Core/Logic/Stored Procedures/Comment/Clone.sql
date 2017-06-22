IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentClone')
BEGIN
	PRINT 'Dropping Procedure CommentClone'
	DROP  Procedure CommentClone
END
GO

PRINT 'Creating Procedure CommentClone'
GO

/*********************************************************************************************
**		File: 
**		Name: CommentClone
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

CREATE Procedure dbo.CommentClone
(
		@CommentId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			 				
	,	@StudentId				INT								
	,	@Date					DATETIME						
	,	@EventTypeId			INT								
	,	@Comment				VARCHAR(500)				
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Comment'			
)
AS
BEGIN

	IF @CommentId IS NULL OR @CommentId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommentId OUTPUT
	END	
		
	
	SELECT	@ApplicationId	=	ApplicationId
		,	@StudentId		=	StudentId
        ,	@Date			=	Date
		,	@EventTypeId	=	EventTypeId
		,	@Comment		=	Comment				
	FROM	dbo.Comment
	WHERE	CommentId		= @CommentId 
	AND     ApplicationId	= @ApplicationId

	EXEC dbo.CommentInsert 
			@CommentId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@StudentId			=	@StudentId
        ,	@Date				=	@Date
		,	@EventTypeId		=	@EventTypeId
		,	@Comment			=	@Comment
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
