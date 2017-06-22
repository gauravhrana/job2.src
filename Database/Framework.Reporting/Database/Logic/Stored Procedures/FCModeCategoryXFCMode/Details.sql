CREATE PROCEDURE dbo.FCModeCategoryXFCModeDetails
(
		@FCModeCategoryXFCModeId				INT			= NULL	
	,	@ApplicationId							INT			= NULL
	,	@FieldConfigurationModeCategoryId		INT			= NULL	
	,	@FieldConfigurationModeId				INT			= NULL	
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@FCModeCategoryXFCModeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.FCModeCategoryXFCModeId	
		,	a.ApplicationId	
		,	a.FieldConfigurationModeCategoryId						
		,	a.FieldConfigurationModeId								
		,	b.Name				AS	'FieldConfigurationModeCategory'			
		,	c.Name				AS	'FieldConfigurationMode'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.FCModeCategoryXFCMode	a
	INNER JOIN	dbo.FieldConfigurationModeCategory			b	ON	a.FieldConfigurationModeCategoryId	=	b.FieldConfigurationModeCategoryId
	INNER JOIN	dbo.FieldConfigurationMode					c	ON	a.FieldConfigurationModeId			=	c.FieldConfigurationModeId
	WHERE	a.FCModeCategoryXFCModeId			=	ISNULL(@FCModeCategoryXFCModeId,	a.FCModeCategoryXFCModeId)	
	AND		a.FieldConfigurationModeCategoryId	=	ISNULL(@FieldConfigurationModeCategoryId, a.FieldConfigurationModeCategoryId)
	AND		a.FieldConfigurationModeId			=	ISNULL(@FieldConfigurationModeId,	a.FieldConfigurationModeId)
	
		-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO

