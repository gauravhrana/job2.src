/******************************************************************************
**		File: 
**		Name: FCModeCategoryXFCModeUpdate
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

CREATE PROCEDURE dbo.FCModeCategoryXFCModeUpdate
(
		@FCModeCategoryXFCModeId				INT		
	,	@ApplicationId							INT
	,	@FieldConfigurationModeCategoryId		INT					
	,	@FieldConfigurationModeId				INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN 

	UPDATE	dbo.FCModeCategoryXFCMode 
	SET		FieldConfigurationModeCategoryId			=	@FieldConfigurationModeCategoryId		
		,	FieldConfigurationModeId					=	@FieldConfigurationModeId							
	WHERE	FCModeCategoryXFCModeId			=	@FCModeCategoryXFCModeId
	AND		ApplicationId					=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

GO

