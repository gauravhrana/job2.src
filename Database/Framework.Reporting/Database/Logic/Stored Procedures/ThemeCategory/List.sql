IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryList')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryList'
	DROP  Procedure  dbo.ThemeCategoryList
END
GO

PRINT 'Creating Procedure ThemeCategoryList'
GO

/******************************************************************************
**		File: 
**		Name: ThemeCategoryList
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

CREATE Procedure dbo.ThemeCategoryList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeCategory'
)
AS
BEGIN

	SELECT	a.ThemeCategoryId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.ThemeCategory a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.ThemeCategoryId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO