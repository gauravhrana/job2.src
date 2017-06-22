IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationGetDetailsViewColumns')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationGetDetailsViewColumns'
	DROP  Procedure FieldConfigurationGetDetailsViewColumns
END
GO

PRINT 'Creating Procedure FieldConfigurationGetDetailsViewColumns'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationGetDetailsViewColumns
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
			EXEC FieldConfigurationGetDetailsViewColumns NULL	, NULL	, NULL
			EXEC FieldConfigurationGetDetailsViewColumns NULL	, 'K'	, NULL
			EXEC FieldConfigurationGetDetailsViewColumns 1	, 'K'	, NULL
			EXEC FieldConfigurationGetDetailsViewColumns 1	, NULL	, NULL
			EXEC FieldConfigurationGetDetailsViewColumns NULL	, NULL	, 'W'

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

Create procedure FieldConfigurationGetDetailsViewColumns
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
		,	a.ApplicationId
		,	a.DisplayColumn
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'
		,	b.Value				AS 'FieldConfigurationDisplayName'
	FROM	dbo.FieldConfiguration a
	INNER JOIN	dbo.FieldConfigurationDisplayName b
		ON	a.FieldConfigurationId = b.FieldConfigurationId
	WHERE	a.SystemEntityTypeId = @SystemEntityTypeId
	AND		a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.DetailsViewPriority >  0
	AND		b.IsDefault		=	1
	ORDER BY a.DetailsViewPriority ASC


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END


GO

