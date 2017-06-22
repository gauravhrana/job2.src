IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DevelopmentCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategoryInsert'
	DROP  Procedure DevelopmentCategoryInsert
END
GO

PRINT 'Creating Procedure DevelopmentCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DevelopmentCategoryInsert
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
CREATE Procedure dbo.DevelopmentCategoryInsert
(
		@DevelopmentCategoryId		INT				= NULL 	OUTPUT
	,	@ApplicationId					INT				= NULL		
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)					
	,	@SortOrder						INT									
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'DevelopmentCategory'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DevelopmentCategoryId OUTPUT, @AuditId

	DECLARE		@DateCreated		AS		DATETIME
	DECLARE		@DateModified		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @DateCreated		= GETDATE() 
	SET @DateModified		= @DateCreated
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	
	INSERT INTO dbo.DevelopmentCategory 
	( 
			DevelopmentCategoryId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder	
		,	DateCreated
		,	DateModified
		,	CreatedByAuditId
		,	ModifiedByAuditId
			
	)
	VALUES 
	(  
			@DevelopmentCategoryId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder	
		,	@DateCreated
		,	@DateModified
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @DevelopmentCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DevelopmentCategory'
		,	@EntityKey				= @DevelopmentCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 