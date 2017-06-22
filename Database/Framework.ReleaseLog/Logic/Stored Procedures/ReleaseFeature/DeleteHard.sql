IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseFeatureDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseFeatureDeleteHard'
	DROP  Procedure ReleaseFeatureDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseFeatureDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseFeatureDelete
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
CREATE Procedure dbo.ReleaseFeatureDeleteHard
(
		@KeyId 					INT		
	,	@ReleaseFeatureId		INT				= NULL				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		 
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseFeature'
)
AS
BEGIN

	IF @KeyType = 'ReleaseFeatureId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'ReleaseFeatureId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.ReleaseFeature
		WHERE	 ReleaseFeatureId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
