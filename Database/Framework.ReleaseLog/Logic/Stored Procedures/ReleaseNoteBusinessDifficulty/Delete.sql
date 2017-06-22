IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessDifficultyDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessDifficultyDelete'
	DROP  Procedure ReleaseNoteBusinessDifficultyDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessDifficultyDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessDifficultyDelete
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
CREATE Procedure dbo.ReleaseNoteBusinessDifficultyDelete
(
		@ReleaseNoteBusinessDifficultyId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteBusinessDifficulty'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteBusinessDifficulty
	WHERE	 ReleaseNoteBusinessDifficultyId = @ReleaseNoteBusinessDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteBusinessDifficulty'
		,	@EntityKey				= @ReleaseNoteBusinessDifficultyId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
