IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SystemEntityXSystemEntityCategorySearch')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategorySearch'
	DROP Procedure SystemEntityXSystemEntityCategorySearch
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategorySearch'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityXSystemEntityCategorySearch
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
			EXEC SystemEntityXSystemEntityCategorySearch NULL	, NULL	, NULL
			EXEC SystemEntityXSystemEntityCategorySearch NULL	, 'K'	, NULL
			EXEC SystemEntityXSystemEntityCategorySearch 1		, 'K'	, NULL
			EXEC SystemEntityXSystemEntityCategorySearch 1		, NULL	, NULL
			EXEC SystemEntityXSystemEntityCategorySearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.SystemEntityXSystemEntityCategorySearch
(
		@SystemEntityXSystemEntityCategoryId	INT				= NULL 	
	,	@SystemEntityId							INT				= NULL 	
	,	@SystemEntityCategoryId					INT				= NULL 
	,	@ApplicationId							INT				= NULL 		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'SystemEntityXSystemEntityCategory'		 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON		
	SELECT	a.SystemEntityXSystemEntityCategoryId												
		,	a.SystemEntityId																
		,	a.SystemEntityCategoryId
		,	b.EntityName					AS	'SystemEntity'
		,	c.Name							AS 'SystemEntityCategory'
	FROM		dbo.SystemEntityXSystemEntityCategory	a
	INNER JOIN	dbo.SystemEntityType				b ON a.SystemEntityId				= b.SystemEntityTypeId
	INNER JOIN	dbo.SystemEntityCategory			c ON a.SystemEntityCategoryId		= c.SystemEntityCategoryId
	WHERE	a.SystemEntityId						= ISNULL(@SystemEntityId, a.SystemEntityId)
	AND		a.SystemEntityCategoryId				= ISNULL(@SystemEntityCategoryId, a.SystemEntityCategoryId)
	AND		a.SystemEntityXSystemEntityCategoryId	= ISNULL(@SystemEntityXSystemEntityCategoryId, a.SystemEntityXSystemEntityCategoryId)
	AND		a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId)
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityXSystemEntityCategory'
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

