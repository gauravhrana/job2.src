IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailInsert')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailInsert'
	DROP  Procedure SearchKeyDetailInsert
END
GO

PRINT 'Creating Procedure SearchKeyDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SearchKeyDetailInsert
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

CREATE Procedure dbo.SearchKeyDetailInsert
(
		@SearchKeyDetailId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@SearchParameter			VARCHAR(200)	
	,	@SearchKeyId				INT	
	,	@SortOrder					INT		
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetail'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SearchKeyDetailId OUTPUT, @AuditId

	INSERT INTO dbo.SearchKeyDetail 
	( 
			ApplicationId
		,	SearchParameter
		,	SearchKeyId						
		,	SortOrder
	)
	VALUES 
	(  
			@ApplicationId
		,	@SearchParameter									
		,	@SearchKeyId
		,	@SortOrder

	)

	SET		@SearchKeyDetailId = SCOPE_IDENTITY()

	SELECT	@SearchKeyDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 