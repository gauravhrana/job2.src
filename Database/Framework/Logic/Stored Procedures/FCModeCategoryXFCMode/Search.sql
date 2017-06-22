/******************************************************************************
**		File: 
**		Name: FCModeCategoryXFCModeSearch
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
			EXEC FCModeCategoryXFCModeSearch NULL	, NULL	, NULL
			EXEC FCModeCategoryXFCModeSearch NULL	, 'K'	, NULL
			EXEC FCModeCategoryXFCModeSearch 1		, 'K'	, NULL
			EXEC FCModeCategoryXFCModeSearch 1		, NULL	, NULL
			EXEC FCModeCategoryXFCModeSearch NULL	, NULL	, 'W'

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
Create procedure FCModeCategoryXFCModeSearch
(
		@FCModeCategoryXFCModeId				INT				= NULL	
	,	@FieldConfigurationModeCategoryId		INT				= NULL	
	,	@FieldConfigurationModeId				INT				= NULL	
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'FCModeCategoryXFCMode' 
)
AS
BEGIN
	SELECT	a.FCModeCategoryXFCModeId	
		,	a.ApplicationId	
		,	a.FieldConfigurationModeCategoryId						
		,	a.FieldConfigurationModeId								
		,	b.Name		AS	'FieldConfigurationModeCategory'			
		,	c.Name		AS	'FieldConfigurationMode'
	FROM		dbo.FCModeCategoryXFCMode	a
	INNER JOIN	FieldConfigurationModeCategory				b	ON	a.FieldConfigurationModeCategoryId	=	b.FieldConfigurationModeCategoryId
	INNER JOIN	FieldConfigurationMode						c	ON	a.FieldConfigurationModeId	=	c.FieldConfigurationModeId
	WHERE   a.FCModeCategoryXFCModeId = ISNULL(@FCModeCategoryXFCModeId, a.FCModeCategoryXFCModeId )
	AND		a.FieldConfigurationModeCategoryId = ISNULL(@FieldConfigurationModeCategoryId, a.FieldConfigurationModeCategoryId )
	AND		a.FieldConfigurationModeId = ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId )
	ORDER BY a.FCModeCategoryXFCModeId	ASC


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO

