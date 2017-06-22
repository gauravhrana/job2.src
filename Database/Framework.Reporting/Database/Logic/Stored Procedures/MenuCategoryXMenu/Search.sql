IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='MenuCategoryXMenuSearch')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuSearch'
	DROP Procedure MenuCategoryXMenuSearch
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuSearch'
GO

/******************************************************************************
**		File: 
**		Name: MenuCategoryXMenuSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC MenuCategoryXMenuSearch NULL	, NULL	, NULL
			EXEC MenuCategoryXMenuSearch NULL	, 'K'	, NULL
			EXEC MenuCategoryXMenuSearch 1		, 'K'	, NULL
			EXEC MenuCategoryXMenuSearch 1		, NULL	, NULL
			EXEC MenuCategoryXMenuSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure MenuCategoryXMenuSearch
(
		@MenuCategoryXMenuId		INT				= NULL
	,	@ApplicationId				INT				= NULL	
	,	@MenuId						INT				= NULL	
	,	@MenuCategoryId				INT				= NULL	
	,	@MenuCategory				VARCHAR(50)		= NULL
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MenuCategoryXMenu' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	
	-- if the MenuCategory did not provide any values
	-- assume search on all possiblities ('%')
	SET @MenuCategory	= ISNULL(@MenuCategory, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@MenuCategory))) = 0
		BEGIN
			SET	@MenuCategory = '%'
		END

	SELECT	a.MenuCategoryXMenuId	
		,	a.ApplicationId	
		,	a.MenuId						
		,	a.MenuCategoryId								
		,	b.Name		AS	'Menu'			
		,	c.Name		AS	'MenuCategory'
	FROM		dbo.MenuCategoryXMenu	a
	INNER JOIN	Menu				b	ON	a.MenuId	=	b.MenuId
	INNER JOIN	MenuCategory						c	ON	a.MenuCategoryId	=	c.MenuCategoryId
	WHERE   a.MenuCategoryXMenuId	= ISNULL(@MenuCategoryXMenuId, a.MenuCategoryXMenuId )
	AND		a.MenuId				= ISNULL(@MenuId, a.MenuId )
	AND		c.Name					LIKE @MenuCategory	+ '%'
	AND		a.MenuCategoryId		= ISNULL(@MenuCategoryId, a.MenuCategoryId )
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.MenuCategoryXMenuId	ASC
	IF @AddAuditInfo = 1 
	BEGIN	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

