/*********************************************************************************************
**		File: 
**		Name:ApplicationModeXFieldConfigurationModeInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ApplicationModeXFieldConfigurationModeInsert
(
		@ApplicationModeXFieldConfigurationModeId	INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@ApplicationModeId							INT								
	,	@FieldConfigurationModeId					INT								
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationModeXFieldConfigurationModeId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationModeXFieldConfigurationMode 
	( 
			ApplicationModeXFieldConfigurationModeId		
		,	ApplicationId			
		,	ApplicationModeId					
		,	FieldConfigurationModeId						
	)
	VALUES 
	(  
			@ApplicationModeXFieldConfigurationModeId		
		,	@ApplicationId			
		,	@ApplicationModeId			
		,	@FieldConfigurationModeId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

