IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleasePublishCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleasePublishCategoryDeleteHard'
	DROP  Procedure ReleasePublishCategoryDeleteHard
END
GO

PRINT 'Creating Procedure ReleasePublishCategoryDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ReleasePublishCategoryDelete
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReleasePublishCategoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ReleasePublishCategory'	
)
AS
BEGIN
		IF @KeyType = 'ReleasePublishCategoryId'
		BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'ReleasePublishCategoryId'  
			,	@AuditId	=	@AuditId

		DELETE	 dbo.ReleasePublishCategory
		WHERE	 ReleasePublishCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
