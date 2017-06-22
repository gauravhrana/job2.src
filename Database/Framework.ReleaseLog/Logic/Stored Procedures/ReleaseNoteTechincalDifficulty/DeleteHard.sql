IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteTechnicalDifficultyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteTechnicalDifficultyDeleteHard'
	DROP  Procedure ReleaseNoteTechnicalDifficultyDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteTechnicalDifficultyDeleteHard'
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
CREATE Procedure dbo.ReleaseNoteTechnicalDifficultyDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteTechnicalDifficultyId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteTechnicalDifficulty'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteTechnicalDifficultyId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteTechnicalDifficultyId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteTechnicalDifficulty
		WHERE	 ReleaseNoteTechnicalDifficultyId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
