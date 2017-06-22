IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure SystemEntityCategoryDelete'
	DROP  Procedure SystemEntityCategoryDelete
END
GO

PRINT 'Creating Procedure SystemEntityCategoryDelete'
GO
/******************************************************************************
**		File: 
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SystemEntityCategoryDelete
(
		@SystemEntityCategoryId 			    INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityCategory'
)
AS
BEGIN

	DELETE	 dbo.SystemEntityCategory
	WHERE	 SystemEntityCategoryId = @SystemEntityCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityCategory'
		,	@EntityKey				= @SystemEntityCategoryId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
