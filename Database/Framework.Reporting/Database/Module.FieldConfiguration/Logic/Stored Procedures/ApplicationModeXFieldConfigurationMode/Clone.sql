
/*********************************************************************************************
**		File: 
**		Name: ApplicationModeXFieldConfigurationModeClone
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

CREATE Procedure dbo.ApplicationModeXFieldConfigurationModeClone
(
		@ApplicationModeXFieldConfigurationModeId				INT			= NULL	
	,	@ApplicationId													INT			= NULL
	,	@ApplicationModeId												INT			= NULL	
	,	@FieldConfigurationModeId								INT			= NULL	
	,	@AuditId														INT					
	,	@AuditDate														DATETIME	= NULL
	,	@SystemEntityType												VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
)
AS
BEGIN		
	
	SELECT	@ApplicationId								= ApplicationId
		,	@ApplicationModeId							= ApplicationModeId
		,	@FieldConfigurationModeId					= FieldConfigurationModeId				
	FROM	dbo.ApplicationModeXFieldConfigurationMode
	WHERE	ApplicationModeXFieldConfigurationModeId	= @ApplicationModeXFieldConfigurationModeId
	ORDER BY ApplicationModeXFieldConfigurationModeId

	EXEC dbo.ApplicationModeXFieldConfigurationModeInsert 
			@ApplicationModeXFieldConfigurationModeId		=	NULL
		,	@ApplicationId						=	@ApplicationId
		,	@ApplicationModeId					=	@ApplicationModeId
		,	@FieldConfigurationModeId			=	@FieldConfigurationModeId
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	

GO

