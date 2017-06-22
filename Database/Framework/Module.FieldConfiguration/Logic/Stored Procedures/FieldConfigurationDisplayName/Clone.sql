IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDisplayNameClone')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameClone'
	DROP  Procedure FieldConfigurationDisplayNameClone
END
GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationDisplayNameClone
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

CREATE Procedure dbo.FieldConfigurationDisplayNameClone
(
		@FieldConfigurationDisplayNameId		INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@LanguageId				INT	
	,	@FieldConfigurationId					INT	
	,	@Value					VARCHAR(50)		
	,	@IsDefault				INT						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FieldConfigurationDisplayName'
)
AS
BEGIN

	IF @FieldConfigurationDisplayNameId IS NULL OR @FieldConfigurationDisplayNameId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'FieldConfigurationDisplayName', @FieldConfigurationDisplayNameId OUTPUT
	END			
	
	SELECT	@ApplicationId  =	ApplicationId
		,	@Value			=	Value				
		,	@FieldConfigurationId			=	FieldConfigurationId				
		,	@LanguageId		=	LanguageId	
		,	@IsDefault		=	IsDefault								
	FROM	dbo.FieldConfigurationDisplayName 
	WHERE	FieldConfigurationDisplayNameId = @FieldConfigurationDisplayNameId

	EXEC dbo.FieldConfigurationDisplayNameInsert 
			@FieldConfigurationDisplayNameId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Value				=	@Value
		,	@FieldConfigurationId				=	@FieldConfigurationId
		,	@LanguageId			=	@LanguageId
		,	@IsDefault			=	@IsDefault
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationDisplayNameId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
