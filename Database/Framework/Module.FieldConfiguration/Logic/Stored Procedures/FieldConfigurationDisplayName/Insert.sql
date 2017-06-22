IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDisplayNameInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameInsert'
	DROP  Procedure FieldConfigurationDisplayNameInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationDisplayNameInsert
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
**		
**********************************************************************************************/

CREATE Procedure dbo.FieldConfigurationDisplayNameInsert
(
		@FieldConfigurationDisplayNameId		INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@LanguageId				INT	
	,	@FieldConfigurationId					INT	
	,	@Value					VARCHAR(50)		
	,	@IsDefault				INT	
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'FieldConfigurationDisplayName'

)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationDisplayNameId OUTPUT, @AuditId

	SET NOCOUNT ON
	
	INSERT INTO dbo.FieldConfigurationDisplayName 
	( 
			ApplicationId		
		,	LanguageId				
		,	FieldConfigurationId	
		,	Value	
		,	IsDefault			
	)
	VALUES 
	(  
			@ApplicationId	
		,	@LanguageId	
		,	@FieldConfigurationId			
		,	@Value		
		,	@IsDefault				
	)
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationDisplayNameId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO

