IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteDeveloperValueDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteDeveloperValueDeleteHard'
	DROP  Procedure ReleaseNoteDeveloperValueDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseNoteDeveloperValueDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteDeveloperValueDelete
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
CREATE Procedure dbo.ReleaseNoteDeveloperValueDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseNoteDeveloperValueId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseNoteDeveloperValue'
)
AS
BEGIN

	IF @KeyType = 'ReleaseNoteDeveloperValueId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseNoteDeveloperValueId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseNoteDeveloperValue
		WHERE	 ReleaseNoteDeveloperValueId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
