IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationUserInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationUserInsert'
	DROP  Procedure  FieldConfigurationModeXApplicationUserInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationUserInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeXApplicationUserInsert
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

CREATE PROCEDURE dbo.FieldConfigurationModeXApplicationUserInsert
(
		@FieldConfigurationModeXApplicationUserId	INT		= NULL 	OUTPUT	
	,	@ApplicationId								INT
	,	@FieldConfigurationModeId					INT								
	,	@ApplicationUserId							INT
	,	@FieldConfigurationModeAccessModeId			INT									
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationUser'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeXApplicationUserId OUTPUT, @AuditId
	
	INSERT INTO dbo.FieldConfigurationModeXApplicationUser 
	( 
			FieldConfigurationModeXApplicationUserId		
		,	ApplicationId			
		,	FieldConfigurationModeId					
		,	ApplicationUserId
		,	FieldConfigurationModeAccessModeId						
	)
	VALUES 
	(  
			@FieldConfigurationModeXApplicationUserId		
		,	@ApplicationId			
		,	@FieldConfigurationModeId			
		,	@ApplicationUserId			
		,	@FieldConfigurationModeAccessModeId
	)

	SELECT @FieldConfigurationModeXApplicationUserId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeXApplicationUserId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

