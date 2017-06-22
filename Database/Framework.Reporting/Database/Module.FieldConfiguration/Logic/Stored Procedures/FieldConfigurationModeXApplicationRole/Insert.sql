IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationRoleInsert'
	DROP  Procedure  FieldConfigurationModeXApplicationRoleInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeXApplicationRoleInsert
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

CREATE PROCEDURE dbo.FieldConfigurationModeXApplicationRoleInsert
(
		@FieldConfigurationModeXApplicationRoleId	INT		= NULL 	OUTPUT	
	,	@ApplicationId								INT
	,	@FieldConfigurationModeId					INT								
	,	@ApplicationRoleId							INT	
	,	@FieldConfigurationModeAccessModeId			INT							
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationRole'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeXApplicationRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.FieldConfigurationModeXApplicationRole 
	( 
			FieldConfigurationModeXApplicationRoleId		
		,	ApplicationId			
		,	FieldConfigurationModeId					
		,	ApplicationRoleId	
		,	FieldConfigurationModeAccessModeId					
	)
	VALUES 
	(  
			@FieldConfigurationModeXApplicationRoleId		
		,	@ApplicationId			
		,	@FieldConfigurationModeId			
		,	@ApplicationRoleId
		,	@FieldConfigurationModeAccessModeId			
	)

	SELECT @FieldConfigurationModeXApplicationRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeXApplicationRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

