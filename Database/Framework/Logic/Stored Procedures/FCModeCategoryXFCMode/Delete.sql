
/******************************************************************************
**		File: 
**		Name: FCModeCategoryXFCModeDelete
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
CREATE Procedure dbo.FCModeCategoryXFCModeDelete
(
		@FCModeCategoryXFCModeId			INT			= NULL	
	,	@FieldConfigurationModeCategoryId	INT			= NULL	
	,	@FieldConfigurationModeId			INT			= NULL	
	,	@AuditId							INT					
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN

	DELETE	dbo.FCModeCategoryXFCMode
	WHERE	FCModeCategoryXFCModeId				=	ISNULL(@FCModeCategoryXFCModeId,	FCModeCategoryXFCModeId)	
	AND		FieldConfigurationModeCategoryId	=   ISNULL(@FieldConfigurationModeCategoryId,	FieldConfigurationModeCategoryId)
	AND		FieldConfigurationModeId			=	ISNULL(@FieldConfigurationModeId,			FieldConfigurationModeId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO


