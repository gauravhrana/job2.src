/******************************************************************************
**		File: 
**		Name: FieldConfigurationGetGridViewColumns
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
			EXEC FieldConfigurationGetGridViewColumns NULL	, NULL	, NULL
			EXEC FieldConfigurationGetGridViewColumns NULL	, 'K'	, NULL
			EXEC FieldConfigurationGetGridViewColumns 1	, 'K'	, NULL
			EXEC FieldConfigurationGetGridViewColumns 1	, NULL	, NULL
			EXEC FieldConfigurationGetGridViewColumns NULL	, NULL	, 'W'

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

Create procedure FieldConfigurationGetGridViewColumns
(
		@FieldConfigurationId			INT				= NULL 				
	,	@Name							VARCHAR(50)		= NULL 
	,	@SystemEntityTypeId				INT				= NULL			
	,	@Value					        VARCHAR(50)		= NULL 				
	,	@Width							NUMERIC(7,2)	= NULL
	,	@Formatting					    VARCHAR(50)		= NULL 				
	,	@ControlType                    VARCHAR(50)		= NULL 
	,	@HorizontalAlignment			VARCHAR(50)		= NULL				
	,	@ApplicationId					INT				= NULL
	,	@AuditId						INT				= NULL
	,	@FieldConfigurationModeId		INT				= NULL
	,	@AuditDate						DATETIME		= NULL
	,	@SystemEntityType				VARCHAR(50)		= 'FieldConfiguration'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
		@EntityKey				=	@FieldConfigurationId
	,	@SystemEntityType		=	@SystemEntityType
	,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
	,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
	,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.FieldConfigurationId							
		,	a.Name									
		,	a.Value									
		,	a.SystemEntityTypeId						
		,	a.Width									
		,	a.Formatting								
		,	a.ControlType		
		,	a.HorizontalAlignment
		,	a.FieldConfigurationModeId
		,	a.ApplicationId
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'	
		,	b.Value				AS 'FieldConfigurationDisplayName'
	FROM	dbo.FieldConfiguration a
	INNER JOIN	dbo.FieldConfigurationDisplayName b
		ON	a.FieldConfigurationId = b.FieldConfigurationId
	WHERE	a.SystemEntityTypeId								= @SystemEntityTypeId
	AND		a.ApplicationId									= ISNULL(@ApplicationId, a.ApplicationId)
	AND		ISNULL(a.FieldConfigurationModeId, -1)	= ISNULL(@FieldConfigurationModeId, ISNULL(a.FieldConfigurationModeId, -1))
	AND		a.GridViewPriority >  0
	AND		b.IsDefault		=	1
	ORDER BY a.GridViewPriority ASC,
			 a.FieldConfigurationModeId DESC


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END


GO

