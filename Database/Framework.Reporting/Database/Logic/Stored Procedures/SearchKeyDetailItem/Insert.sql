IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemInsert')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemInsert'
	DROP  Procedure SearchKeyDetailItemInsert
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SearchKeyDetailItemInsert
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
**		Date:		Author:				Value:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/
CREATE Procedure dbo.SearchKeyDetailItemInsert
(
		@SearchKeyDetailItemId		INT				= NULL 	OUTPUT	
	,	@ApplicationId				INT			
	,	@SearchKeyDetailId			INT
	,	@Value						VARCHAR(200)							
	,	@SortOrder					INT									
	,	@AuditId					INT										
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetailItem'
)
AS
BEGIN
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SearchKeyDetailItemId OUTPUT, @AuditId
	
	INSERT INTO dbo.SearchKeyDetailItem 
	( 
			ApplicationId							
		,	SearchKeyDetailId						
		,	Value						
		,	SortOrder						
	)
	VALUES 
	(  
			@ApplicationId		
		,	@SearchKeyDetailId						
		,	@Value					
		,	@SortOrder			
	)

	SET @SearchKeyDetailItemId = SCOPE_IDENTITY()

	SELECT @SearchKeyDetailItemId
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 