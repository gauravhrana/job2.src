IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyInsert')
BEGIN
	PRINT 'Dropping Procedure SuperKeyInsert'
	DROP  Procedure SuperKeyInsert
END
GO

PRINT 'Creating Procedure SuperKeyInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SuperKeyInsert
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

CREATE Procedure dbo.SuperKeyInsert
(
		@SuperKeyId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT			
	,	@SystemEntityTypeId			INT			
	,	@ExpirationDate				DATETIME			
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKey'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SuperKeyId OUTPUT, @AuditId

	DECLARE @MyName AS VARCHAR(500)

	IF @Name IS NULL OR @Name = ''
		BEGIN
			
			DECLARE @MaxId AS INT
			
			SELECT	@MaxId = MAX(a.SuperKeyId) 
			FROM	dbo.SuperKey	a

			SET		@MaxId = @MaxId + 1
			
			SET		@MyName = CAST(@MaxId AS VARCHAR(50))

		END
	ELSE
		BEGIN
			SET @MyName = @Name
		END


	INSERT INTO dbo.SuperKey 
	( 
			ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder
		,	SystemEntityTypeId
		,	ExpirationDate						
	)
	VALUES 
	(  
			@ApplicationId	
		,	@MyName						
		,	@Description				
		,	@SortOrder
		,	@SystemEntityTypeId
		,	@ExpirationDate									
	)

	SET @SuperKeyId = SCOPE_IDENTITY()

	SELECT @SuperKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 