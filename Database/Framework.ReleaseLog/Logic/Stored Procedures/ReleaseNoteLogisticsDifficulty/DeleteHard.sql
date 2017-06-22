IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteLogisticsDifficultyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteLogisticsDifficultyDeleteHard'
	DROP  Procedure ReleaseNoteLogisticsDifficultyDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteLogisticsDifficultyDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteLogisticsDifficultyDelete
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
CREATE Procedure dbo.ReleaseNoteLogisticsDifficultyDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteLogisticsDifficultyId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteLogisticsDifficulty'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteLogisticsDifficultyId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteLogisticsDifficultyId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteLogisticsDifficulty
		WHERE	 ReleaseNoteLogisticsDifficultyId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
