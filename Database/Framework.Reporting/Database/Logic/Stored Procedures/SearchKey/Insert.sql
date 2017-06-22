IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyInsert')
BEGIN
	PRINT 'Dropping Procedure SearchKeyInsert'
	DROP  Procedure SearchKeyInsert
END
GO

PRINT 'Creating Procedure SearchKeyInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SearchKeyInsert
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

CREATE Procedure dbo.SearchKeyInsert
(
		@SearchKeyId				INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT			
	,	@View						VARCHAR(100)						
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKey'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SearchKeyId OUTPUT, @AuditId

	DECLARE @MyName AS VARCHAR(500)

	IF @Name IS NULL OR @Name = ''
		BEGIN
			
			DECLARE @MaxId AS INT
			
			SELECT	@MaxId = MAX(a.SearchKeyId) 
			FROM	dbo.SearchKey	a

			SET		@MaxId = @MaxId + 1
			
			SET		@MyName = CAST(@MaxId AS VARCHAR(50))

		END
	ELSE
		BEGIN
			SET @MyName = @Name
		END


	INSERT INTO dbo.SearchKey 
	( 
			ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder
		,	[View]		
	)
	VALUES 
	(  
			@ApplicationId	
		,	@MyName						
		,	@Description				
		,	@SortOrder
		,	@View											
	)

	SET @SearchKeyId = SCOPE_IDENTITY()

	SELECT @SearchKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 