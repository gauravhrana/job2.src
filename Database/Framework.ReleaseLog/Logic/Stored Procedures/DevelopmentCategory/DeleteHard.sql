IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DevelopmentCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategoryDeleteHard'
	DROP  Procedure DevelopmentCategoryDeleteHard
END
GO

PRINT 'Creating Procedure DevelopmentCategoryDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: DevelopmentCategoryDelete
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
CREATE Procedure dbo.DevelopmentCategoryDeleteHard
(
		@KeyId 						INT		
	,	@DevelopmentCategoryId		INT				= NULL				
	,	@KeyType					VARCHAR(50)				
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL		 
	,	@SystemEntityType			VARCHAR(50)		= 'DevelopmentCategory'
)
AS
BEGIN

	IF @KeyType = 'DevelopmentCategoryId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'DevelopmentCategoryId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.DevelopmentCategory
		WHERE	 DevelopmentCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
