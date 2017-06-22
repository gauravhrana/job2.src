IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameInsert')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameInsert'
	DROP  Procedure MenuDisplayNameInsert
END
GO

PRINT 'Creating Procedure MenuDisplayNameInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:MenuDisplayNameInsert
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

CREATE Procedure dbo.MenuDisplayNameInsert
(
		@MenuDisplayNameId		INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@LanguageId				INT	
	,	@MenuId					INT	
	,	@Value					VARCHAR(50)		
	,	@IsDefault				INT	
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'MenuDisplayName'

)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MenuDisplayNameId OUTPUT, @AuditId

	SET NOCOUNT ON
	
	INSERT INTO dbo.MenuDisplayName 
	( 
			MenuDisplayNameId
		,	ApplicationId		
		,	LanguageId				
		,	MenuId	
		,	Value	
		,	IsDefault			
	)
	VALUES 
	(  
			@MenuDisplayNameId
		,	@ApplicationId	
		,	@LanguageId	
		,	@MenuId			
		,	@Value		
		,	@IsDefault				
	)
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuDisplayNameId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO

