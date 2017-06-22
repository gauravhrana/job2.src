/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryXFCModeUpdate
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

CREATE PROCEDURE dbo.FieldConfigurationModeCategoryXFCModeUpdate
(
		@FieldConfigurationModeCategoryXFCModeId				INT		
	,	@ApplicationId							INT
	,	@FieldConfigurationModeCategoryId		INT					
	,	@FieldConfigurationModeId				INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'FieldConfigurationModeCategoryXFCMode'
)
AS
BEGIN 

	UPDATE	dbo.FieldConfigurationModeCategoryXFCMode 
	SET		FieldConfigurationModeCategoryId			=	@FieldConfigurationModeCategoryId		
		,	FieldConfigurationModeId					=	@FieldConfigurationModeId							
	WHERE	FieldConfigurationModeCategoryXFCModeId			=	@FieldConfigurationModeCategoryXFCModeId
	AND		ApplicationId					=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FieldConfigurationModeCategoryXFCModeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

GO

