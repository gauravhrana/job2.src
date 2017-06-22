IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeInsert')
BEGIN
	PRINT 'Dropping Procedure ThemeInsert'
	DROP  Procedure ThemeInsert
END
GO

PRINT 'Creating Procedure ThemeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ThemeInsert
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

CREATE Procedure dbo.ThemeInsert
(
		@ThemeId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'Theme'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeId OUTPUT, @AuditId
	
	INSERT INTO dbo.Theme 
	( 
			ThemeId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@ThemeId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ThemeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 