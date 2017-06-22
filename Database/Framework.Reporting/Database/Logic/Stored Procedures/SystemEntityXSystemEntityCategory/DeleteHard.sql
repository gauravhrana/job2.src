IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryDeleteHard'
	DROP  Procedure SystemEntityXSystemEntityCategoryDeleteHard
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: SystemEntityXSystemEntityCategoryDelete
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
CREATE Procedure dbo.SystemEntityXSystemEntityCategoryDeleteHard
(
		@SystemEntityXSystemEntityCategoryId		INT				= NULL		
	,	@KeyId 					INT							
	,	@KeyType				VARCHAR(50)					
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN

	IF @KeyType = 'SystemEntityXSystemEntityCategoryId'
	BEGIN

		DELETE	 dbo.SystemEntityXSystemEntityCategory
		WHERE	 SystemEntityXSystemEntityCategoryId = @KeyId

	END
	ELSE IF @KeyType = 'SystemEntityId'
	BEGIN

		DELETE	 dbo.SystemEntityXSystemEntityCategory
		WHERE	 SystemEntityId = @KeyId

	END
	ELSE IF @KeyType = 'SystemEntityCategoryId'
	BEGIN

		DELETE	 dbo.SystemEntityXSystemEntityCategory
		WHERE	 SystemEntityCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
