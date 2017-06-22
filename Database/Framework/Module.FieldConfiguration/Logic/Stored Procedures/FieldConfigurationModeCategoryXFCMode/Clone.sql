/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryXFCModeClone
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.FieldConfigurationModeCategoryXFCModeClone
(
		@FieldConfigurationModeCategoryXFCModeId			INT			= NULL	
	,	@ApplicationId						INT			= NULL
	,	@FieldConfigurationModeCategoryId	INT			= NULL	
	,	@FieldConfigurationModeId			INT			= NULL	
	,	@AuditId							INT					
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'FieldConfigurationModeCategoryXFCMode'
)
AS
BEGIN		
	
	SELECT	@ApplicationId						= ApplicationId
		,	@FieldConfigurationModeCategoryId	= FieldConfigurationModeCategoryId
		,	@FieldConfigurationModeId			= FieldConfigurationModeId				
	FROM	dbo.FieldConfigurationModeCategoryXFCMode
	WHERE	FieldConfigurationModeCategoryXFCModeId	= @FieldConfigurationModeCategoryXFCModeId
	ORDER BY FieldConfigurationModeCategoryXFCModeId

	EXEC dbo.FieldConfigurationModeCategoryXFCModeInsert 
			@FieldConfigurationModeCategoryXFCModeId				=	NULL
		,	@ApplicationId							=	@ApplicationId
		,	@FieldConfigurationModeCategoryId		=	@FieldConfigurationModeCategoryId
		,	@FieldConfigurationModeId				=	@FieldConfigurationModeId
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeCategoryXFCModeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	

GO

