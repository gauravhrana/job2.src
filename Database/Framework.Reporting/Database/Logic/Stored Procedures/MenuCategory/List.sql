IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryList')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryList'
	DROP  Procedure  dbo.MenuCategoryList
END
GO

PRINT 'Creating Procedure MenuCategoryList'
GO

/******************************************************************************
**		File: 
**		Name: MenuCategoryList
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
**     ----------					   ---------
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

CREATE Procedure dbo.MenuCategoryList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MenuCategory'
)
AS
BEGIN

	SELECT	a.MenuCategoryId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.MenuCategory a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.MenuCategoryId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO