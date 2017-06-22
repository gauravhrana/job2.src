IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDisplayNameUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameUpdate'
	DROP  Procedure  FieldConfigurationDisplayNameUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationDisplayNameUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.FieldConfigurationDisplayNameUpdate
(
		@FieldConfigurationDisplayNameId		INT		
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

	SET NOCOUNT ON
	
	UPDATE	dbo.FieldConfigurationDisplayName 
	SET		Value				=	@Value					
		,	FieldConfigurationId				=	@FieldConfigurationId			
		,	LanguageId			=	@LanguageId
		,	IsDefault			=	@IsDefault				
	WHERE	FieldConfigurationDisplayNameId	=	@FieldConfigurationDisplayNameId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationDisplayNameId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO