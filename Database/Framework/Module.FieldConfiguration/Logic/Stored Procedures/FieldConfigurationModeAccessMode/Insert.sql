IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeInsert'
	DROP  Procedure FieldConfigurationModeAccessModeInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeAccessModeInsert
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/
CREATE Procedure dbo.FieldConfigurationModeAccessModeInsert
(
		@FieldConfigurationModeAccessModeId		INT				= NULL 	OUTPUT
	,	@ApplicationId							INT				= NULL		
	,	@Name									VARCHAR(100)						
	,	@Description							VARCHAR (500)						
	,	@SortOrder								INT								
	,	@AuditId								INT									
	,	@AuditDate								DATETIME		= NULL				
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationModeAccessMode'
)
AS
BEGIN

	IF @FieldConfigurationModeAccessModeId IS NULL OR @FieldConfigurationModeAccessModeId = -999999
	BEGIN
	 	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeAccessModeId OUTPUT, @AuditId
	END
	
	
	INSERT INTO dbo.FieldConfigurationModeAccessMode 
	( 
			FieldConfigurationModeAccessModeId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder		
	)
	VALUES 
	(  
			@FieldConfigurationModeAccessModeId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeAccessModeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO