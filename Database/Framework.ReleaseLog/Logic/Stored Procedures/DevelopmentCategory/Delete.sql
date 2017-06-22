IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DevelopmentCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategoryDelete'
	DROP  Procedure DevelopmentCategoryDelete
END
GO

PRINT 'Creating Procedure DevelopmentCategoryDelete'
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
CREATE Procedure dbo.DevelopmentCategoryDelete
(
		@DevelopmentCategoryId		INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DevelopmentCategory'
)
AS
BEGIN

	DELETE	 dbo.DevelopmentCategory
	WHERE	 DevelopmentCategoryId = @DevelopmentCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DevelopmentCategory'
		,	@EntityKey				= @DevelopmentCategoryId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
