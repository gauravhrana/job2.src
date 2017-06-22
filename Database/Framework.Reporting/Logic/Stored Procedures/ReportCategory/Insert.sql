IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryInsert'
	DROP  Procedure ReportCategoryInsert
END
GO

PRINT 'Creating Procedure ReportCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReportCategoryInsert
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
CREATE Procedure dbo.ReportCategoryInsert
(
		@ReportCategoryId		INT			= NULL 	OUTPUT
	,	@ApplicationId			INT				
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReportCategory'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReportCategoryId OUTPUT, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@ModifiedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @CreatedDate		= GETDATE() 
	SET @ModifiedDate		= @CreatedDate
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId
	
	INSERT INTO dbo.ReportCategory 
	( 
			ReportCategoryId
		,	ApplicationId								
		,	Name		
		,	Description			
		,	SortOrder
		,	CreatedDate
		,	ModifiedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId					
	)
	VALUES 
	(  
			@ReportCategoryId
		,	@ApplicationId				
		,	@Name			
		,	@Description		
		,	@SortOrder	
		,	@CreatedDate
		,	@ModifiedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReportCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 