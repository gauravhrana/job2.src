IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemEntityCategoryDeleteHard'
	DROP  Procedure SystemEntityCategoryDeleteHard
END
GO

PRINT 'Creating Procedure SystemEntityCategoryDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SystemEntityCategoryDelete
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
CREATE Procedure dbo.SystemEntityCategoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityCategory'
)
AS
BEGIN
	IF @KeyType = 'SystemEntityCategoryId'
	BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'SystemEntityCategoryId' 
			,	@AuditId	=	@AuditId

		DELETE	 dbo.SystemEntityCategory
		WHERE	 SystemEntityCategoryId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
