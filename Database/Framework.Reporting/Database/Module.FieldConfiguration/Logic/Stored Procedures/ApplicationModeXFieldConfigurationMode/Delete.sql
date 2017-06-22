/******************************************************************************
**		File: 
**		Name: ApplicationModeXFieldConfigurationModeDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ApplicationModeXFieldConfigurationModeDelete
(
		@ApplicationModeXFieldConfigurationModeId				INT			= NULL	
	,	@ApplicationModeId										INT			= NULL	
	,	@FieldConfigurationModeId								INT			= NULL	
	,	@AuditId												INT					
	,	@AuditDate												DATETIME	= NULL
	,	@SystemEntityType										VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
)
AS
BEGIN

	DELETE	dbo.ApplicationModeXFieldConfigurationMode
	WHERE	ApplicationModeXFieldConfigurationModeId	=	ISNULL(@ApplicationModeXFieldConfigurationModeId,	ApplicationModeXFieldConfigurationModeId)	
	AND		ApplicationModeId									=	ISNULL(@ApplicationModeId,			ApplicationModeId)
	AND		FieldConfigurationModeId					=	ISNULL(@FieldConfigurationModeId,			FieldConfigurationModeId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO

