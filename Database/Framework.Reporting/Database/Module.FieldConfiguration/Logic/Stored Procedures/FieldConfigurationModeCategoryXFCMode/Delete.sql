
/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryXFCModeDelete
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
CREATE Procedure dbo.FieldConfigurationModeCategoryXFCModeDelete
(
		@FieldConfigurationModeCategoryXFCModeId	INT			= NULL	
	,	@FieldConfigurationModeCategoryId			INT			= NULL	
	,	@FieldConfigurationModeId					INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeCategoryXFCMode'
)
AS
BEGIN

	DELETE	dbo.FieldConfigurationModeCategoryXFCMode
	WHERE	FieldConfigurationModeCategoryXFCModeId		=	ISNULL(@FieldConfigurationModeCategoryXFCModeId,	FieldConfigurationModeCategoryXFCModeId)	
	AND		FieldConfigurationModeCategoryId			=   ISNULL(@FieldConfigurationModeCategoryId,	FieldConfigurationModeCategoryId)
	AND		FieldConfigurationModeId					=	ISNULL(@FieldConfigurationModeId,			FieldConfigurationModeId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeCategoryXFCModeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO


