IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseInsert')
BEGIN
	PRINT 'Dropping Procedure TestCaseInsert'
	DROP  Procedure TestCaseInsert
END
GO

PRINT 'Creating Procedure TestCaseInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestCaseInsert
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

CREATE Procedure dbo.TestCaseInsert
(
		@TestCaseId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TestCase'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestCaseId OUTPUT, @AuditId
		
	INSERT INTO dbo.TestCase 
	( 
			TestCaseId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TestCaseId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 