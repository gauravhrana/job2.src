IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleasePublishCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure ReleasePublishCategoryDelete'
	DROP  Procedure ReleasePublishCategoryDelete
END
GO

PRINT 'Creating Procedure ReleasePublishCategoryDelete'
GO
/******************************************************************************
**		File: 
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
CREATE Procedure dbo.ReleasePublishCategoryDelete
(
		@ReleasePublishCategoryId 		    INT						
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL		
	,	@SystemEntityType					VARCHAR(50)	= 'ReleasePublishCategory'
)
AS
BEGIN
		DELETE	 dbo.ReleasePublishCategory
		WHERE	 ReleasePublishCategoryId = @ReleasePublishCategoryId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleasePublishCategory'
		,	@EntityKey				= @ReleasePublishCategoryId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
GO
