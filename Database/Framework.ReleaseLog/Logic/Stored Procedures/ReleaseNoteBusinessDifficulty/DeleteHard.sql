IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessDifficultyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessDifficultyDeleteHard'
	DROP  Procedure ReleaseNoteBusinessDifficultyDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessDifficultyDeleteHard'
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
CREATE Procedure dbo.ReleaseNoteBusinessDifficultyDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteBusinessDifficultyId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteBusinessDifficulty'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteBusinessDifficultyId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteBusinessDifficultyId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteBusinessDifficulty
		WHERE	 ReleaseNoteBusinessDifficultyId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
