/*********************************************************************************************
**		File: 
**		Name:FCModeCategoryXFCModeInsert
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

CREATE PROCEDURE dbo.FCModeCategoryXFCModeInsert
(
		@FCModeCategoryXFCModeId		     INT		= NULL 	OUTPUT	
	,	@ApplicationId						 INT		= NULL	
	,	@FieldConfigurationModeCategoryId	 INT								
	,	@FieldConfigurationModeId			 INT								
	,	@AuditId							 INT									
	,	@AuditDate							 DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FCModeCategoryXFCModeId OUTPUT, @AuditId
	
	INSERT INTO dbo.FCModeCategoryXFCMode 
	( 
			FCModeCategoryXFCModeId		
		,	ApplicationId			
		,	FieldConfigurationModeCategoryId					
		,	FieldConfigurationModeId						
	)
	VALUES 
	(  
			@FCModeCategoryXFCModeId		
		,	@ApplicationId			
		,	@FieldConfigurationModeCategoryId			
		,	@FieldConfigurationModeId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FCModeCategoryXFCModeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

