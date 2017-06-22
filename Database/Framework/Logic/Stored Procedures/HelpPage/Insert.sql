IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageInsert')
BEGIN
	PRINT 'Dropping Procedure HelpPageInsert'
	DROP  Procedure HelpPageInsert
END
GO

PRINT 'Creating Procedure HelpPageInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:HelpPageInsert
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
**		Date:		Author:				Content:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.HelpPageInsert
(
		@HelpPageId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT					
	,	@SystemEntityTypeId			INT				
	,	@HelpPageContextId			INT	
	,	@Name						VARCHAR(50)						
	,	@Content					VARCHAR(MAX)						
	,	@SortOrder					INT	
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @HelpPageId OUTPUT, @AuditId
	
	DECLARE @MyName AS VARCHAR(500)

	IF @Name IS NULL OR @Name = ''
		BEGIN
			
			DECLARE @MaxId AS INT
			
			SELECT	@MaxId = MAX(a.HelpPageId) 
			FROM	dbo.HelpPage	a

			SET		@MaxId = @MaxId + 1
			
			SET		@MyName = CAST(@MaxId AS VARCHAR(50))

		END
	ELSE
		BEGIN
			SET @MyName = @Name
		END


	INSERT INTO dbo.HelpPage 
	( 
			ApplicationId					
		,	Name						
		,	Content					
		,	SortOrder
		,	SystemEntityTypeId		
		,	HelpPageContextId			
	)
	VALUES 
	(  
			@ApplicationId	
		,	@MyName						
		,	@Content				
		,	@SortOrder
		,	@SystemEntityTypeId		
		,	@HelpPageContextId						
	)

	SET @HelpPageId = SCOPE_IDENTITY()

	SELECT @HelpPageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO
