IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleasePublishCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure ReleasePublishCategoryInsert'
	DROP  Procedure ReleasePublishCategoryInsert
END
GO

PRINT 'Creating Procedure ReleasePublishCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleasePublishCategoryInsert
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
CREATE Procedure dbo.ReleasePublishCategoryInsert
(
		@ReleasePublishCategoryId		INT		= NULL 	OUTPUT	
	,	@ApplicationId					INT				
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleasePublishCategory'
)
AS
BEGIN
	
	IF @ReleasePublishCategoryId IS NULL OR @ReleasePublishCategoryId = -999999
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleasePublishCategoryId OUTPUT, @AuditId
		
	INSERT INTO dbo.ReleasePublishCategory 
	( 
			ReleasePublishCategoryId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@ReleasePublishCategoryId
		,	@ApplicationId			
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleasePublishCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 