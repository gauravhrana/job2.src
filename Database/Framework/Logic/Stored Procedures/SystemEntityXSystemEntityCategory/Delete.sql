IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryDelete'
	DROP  Procedure SystemEntityXSystemEntityCategoryDelete
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryDelete'
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
CREATE Procedure dbo.SystemEntityXSystemEntityCategoryDelete
(
		@SystemEntityXSystemEntityCategoryId 		INT			= NULL		
	,	@SystemEntityId 				INT			= NULL		
	,	@SystemEntityCategoryId 				INT			= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN

	DELETE	dbo.SystemEntityXSystemEntityCategory
	WHERE	SystemEntityXSystemEntityCategoryId	=	ISNULL(@SystemEntityXSystemEntityCategoryId,	SystemEntityXSystemEntityCategoryId)	
	AND		SystemEntityId			=	ISNULL(@SystemEntityId,			SystemEntityId)
	AND		SystemEntityCategoryId			=	ISNULL(@SystemEntityCategoryId,			SystemEntityCategoryId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityXSystemEntityCategory'
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
