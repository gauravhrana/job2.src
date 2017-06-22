IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessValueDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessValueDeleteHard'
	DROP  Procedure ReleaseNoteBusinessValueDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessValueDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessValueDelete
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
CREATE Procedure dbo.ReleaseNoteBusinessValueDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteBusinessValueId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteBusinessValue'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteBusinessValueId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteBusinessValueId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteBusinessValue
		WHERE	 ReleaseNoteBusinessValueId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
