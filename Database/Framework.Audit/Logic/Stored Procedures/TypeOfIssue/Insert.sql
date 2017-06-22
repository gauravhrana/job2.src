IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TypeOfIssueInsert')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueInsert'
	DROP  Procedure TypeOfIssueInsert
END
GO

PRINT 'Creating Procedure TypeOfIssueInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TypeOfIssueInsert
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

CREATE Procedure dbo.TypeOfIssueInsert
(
		@TypeOfIssueId			INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT	
	,	@Name					VARCHAR(50)	
	,	@Category				VARCHAR(100)					
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'TypeOfIssue'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TypeOfIssueId OUTPUT, @AuditId
	
	INSERT INTO dbo.TypeOfIssue 
	( 
			TypeOfIssueId	
		,   ApplicationId					
		,	Name		
		,	Category				
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@TypeOfIssueId	
		,   @ApplicationId	
		,	@Name	
		,	@Category					
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TypeOfIssueId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 