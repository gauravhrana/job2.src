IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessDifficultyUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessDifficultyUpdate'
	DROP  Procedure  ReleaseNoteBusinessDifficultyUpdate
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessDifficultyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessDifficultyUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReleaseNoteBusinessDifficultyUpdate
(
		@ReleaseNoteBusinessDifficultyId		INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseNoteBusinessDifficulty'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.ReleaseNoteBusinessDifficulty 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	ReleaseNoteBusinessDifficultyId	=	@ReleaseNoteBusinessDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteBusinessDifficulty'
		,	@EntityKey				= @ReleaseNoteBusinessDifficultyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO