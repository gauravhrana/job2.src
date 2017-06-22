IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FieldConfigurationModeCategoryXFCModeSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryXFCModeSearch'
	DROP Procedure FieldConfigurationModeCategoryXFCModeSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryXFCModeSearch'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryXFCModeSearch
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
			EXEC FieldConfigurationModeCategoryXFCModeSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationModeCategoryXFCModeSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeCategoryXFCModeSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationModeCategoryXFCModeSearch 1		, NULL	, NULL
			EXEC FieldConfigurationModeCategoryXFCModeSearch NULL	, NULL	, 'W'

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
Create procedure FieldConfigurationModeCategoryXFCModeSearch
(
		@FieldConfigurationModeCategoryXFCModeId	INT				= NULL	
	,	@FieldConfigurationModeCategoryId			INT				= NULL	
	,	@FieldConfigurationModeId					INT				= NULL	
	,	@AuditId									INT						
	,	@AuditDate									DATETIME		= NULL
	,	@SystemEntityType							VARCHAR(50)		= 'FieldConfigurationModeCategoryXFCMode' 
	,	@ApplicationMode							INT				= NULL		
	,	@AddAuditInfo								INT				= 1
	,	@AddTraceInfo								INT				= 0
	,	@ReturnAuditInfo							INT				= 0
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	SELECT	a.FieldConfigurationModeCategoryXFCModeId	
		,	a.ApplicationId	
		,	a.FieldConfigurationModeCategoryId						
		,	a.FieldConfigurationModeId								
		,	b.Name		AS	'FieldConfigurationModeCategory'			
		,	c.Name		AS	'FieldConfigurationMode'
	FROM		dbo.FieldConfigurationModeCategoryXFCMode	a
	INNER JOIN	FieldConfigurationModeCategory				b	ON	a.FieldConfigurationModeCategoryId	=	b.FieldConfigurationModeCategoryId
	INNER JOIN	FieldConfigurationMode						c	ON	a.FieldConfigurationModeId	=	c.FieldConfigurationModeId
	WHERE   a.FieldConfigurationModeCategoryXFCModeId = ISNULL(@FieldConfigurationModeCategoryXFCModeId, a.FieldConfigurationModeCategoryXFCModeId )
	AND		a.FieldConfigurationModeCategoryId = ISNULL(@FieldConfigurationModeCategoryId, a.FieldConfigurationModeCategoryId )
	AND		a.FieldConfigurationModeId = ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId )
	ORDER BY a.FieldConfigurationModeCategoryXFCModeId	ASC

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationModeCategoryXFCModeId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
				
		END

END

GO

