IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteTechnicalDifficultyDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteTechnicalDifficultyDelete'
	DROP  Procedure ReleaseNoteTechnicalDifficultyDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteTechnicalDifficultyDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteTechnicalDifficultyDelete
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
CREATE Procedure dbo.ReleaseNoteTechnicalDifficultyDelete
(
		@ReleaseNoteTechnicalDifficultyId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteTechnicalDifficulty'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteTechnicalDifficulty
	WHERE	 ReleaseNoteTechnicalDifficultyId = @ReleaseNoteTechnicalDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteTechnicalDifficulty'
		,	@EntityKey				= @ReleaseNoteTechnicalDifficultyId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
