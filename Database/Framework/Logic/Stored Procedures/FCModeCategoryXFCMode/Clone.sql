/*********************************************************************************************
**		File: 
**		Name: FCModeCategoryXFCModeClone
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

CREATE Procedure dbo.FCModeCategoryXFCModeClone
(
		@FCModeCategoryXFCModeId			INT			= NULL	
	,	@ApplicationId						INT			= NULL
	,	@FieldConfigurationModeCategoryId	INT			= NULL	
	,	@FieldConfigurationModeId			INT			= NULL	
	,	@AuditId							INT					
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN		
	
	SELECT	@ApplicationId						= ApplicationId
		,	@FieldConfigurationModeCategoryId	= FieldConfigurationModeCategoryId
		,	@FieldConfigurationModeId			= FieldConfigurationModeId				
	FROM	dbo.FCModeCategoryXFCMode
	WHERE	FCModeCategoryXFCModeId	= @FCModeCategoryXFCModeId
	ORDER BY FCModeCategoryXFCModeId

	EXEC dbo.FCModeCategoryXFCModeInsert 
			@FCModeCategoryXFCModeId				=	NULL
		,	@ApplicationId							=	@ApplicationId
		,	@FieldConfigurationModeCategoryId		=	@FieldConfigurationModeCategoryId
		,	@FieldConfigurationModeId				=	@FieldConfigurationModeId
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	

GO

