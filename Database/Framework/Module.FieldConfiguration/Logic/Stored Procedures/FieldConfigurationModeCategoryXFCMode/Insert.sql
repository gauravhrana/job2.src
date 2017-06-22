/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeCategoryXFCModeInsert
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

CREATE PROCEDURE dbo.FieldConfigurationModeCategoryXFCModeInsert
(
		@FieldConfigurationModeCategoryXFCModeId		     INT		= NULL 	OUTPUT	
	,	@ApplicationId						 INT		= NULL	
	,	@FieldConfigurationModeCategoryId	 INT								
	,	@FieldConfigurationModeId			 INT								
	,	@AuditId							 INT									
	,	@AuditDate							 DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50)	= 'FieldConfigurationModeCategoryXFCMode'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeCategoryXFCModeId OUTPUT, @AuditId
	
	INSERT INTO dbo.FieldConfigurationModeCategoryXFCMode 
	( 
			FieldConfigurationModeCategoryXFCModeId		
		,	ApplicationId			
		,	FieldConfigurationModeCategoryId					
		,	FieldConfigurationModeId						
	)
	VALUES 
	(  
			@FieldConfigurationModeCategoryXFCModeId		
		,	@ApplicationId			
		,	@FieldConfigurationModeCategoryId			
		,	@FieldConfigurationModeId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeCategoryXFCModeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

