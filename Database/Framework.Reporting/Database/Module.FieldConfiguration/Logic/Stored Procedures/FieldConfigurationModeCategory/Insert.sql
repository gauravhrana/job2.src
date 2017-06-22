IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryInsert'
	DROP  Procedure FieldConfigurationModeCategoryInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationModeCategoryInsert
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
CREATE Procedure dbo.FieldConfigurationModeCategoryInsert
(
		@FieldConfigurationModeCategoryId		INT				= NULL 	OUTPUT
	,	@ApplicationId			INT				= NULL		
	,	@Name					VARCHAR(100)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'FieldConfigurationModeCategory'
)
AS
BEGIN
	IF @FieldConfigurationModeCategoryId IS NULL OR @FieldConfigurationModeCategoryId = -999999
	BEGIN
	 	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeCategoryId OUTPUT, @AuditId
	END
	
	
	INSERT INTO dbo.FieldConfigurationModeCategory 
	( 
			FieldConfigurationModeCategoryId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder		
	)
	VALUES 
	(  
			@FieldConfigurationModeCategoryId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationModeCategory'
		,	@EntityKey				= @FieldConfigurationModeCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO