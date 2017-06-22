IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeInsert'
	DROP  Procedure FieldConfigurationModeInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationModeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeInsert
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
CREATE Procedure dbo.FieldConfigurationModeInsert
(
		@FieldConfigurationModeId		INT				= NULL 	OUTPUT
	,	@ApplicationId			INT				= NULL		
	,	@Name					VARCHAR(100)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'FieldConfigurationMode'
)
AS
BEGIN
	IF @FieldConfigurationModeId IS NULL OR @FieldConfigurationModeId = -999999
	BEGIN
	 	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeId OUTPUT, @AuditId
	END
	
	
	INSERT INTO dbo.FieldConfigurationMode 
	( 
			FieldConfigurationModeId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder		
	)
	VALUES 
	(  
			@FieldConfigurationModeId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationMode'
		,	@EntityKey				= @FieldConfigurationModeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO