IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteQualitativeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteQualitativeDeleteHard'
	DROP  Procedure ReleaseNoteQualitativeDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteQualitativeDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteQualitativeDelete
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
CREATE Procedure dbo.ReleaseNoteQualitativeDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteQualitativeId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteQualitative'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteQualitativeId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteQualitativeId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteQualitative
		WHERE	 ReleaseNoteQualitativeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
