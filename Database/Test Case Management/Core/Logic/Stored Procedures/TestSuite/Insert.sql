IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteInsert')
BEGIN
	PRINT 'Dropping Procedure TestSuiteInsert'
	DROP  Procedure TestSuiteInsert
END
GO

PRINT 'Creating Procedure TestSuiteInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestSuiteInsert
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

CREATE Procedure dbo.TestSuiteInsert
(
		@TestSuiteId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TestSuite'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestSuiteId OUTPUT, @AuditId
		
	INSERT INTO dbo.TestSuite 
	( 
			TestSuiteId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TestSuiteId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestSuiteId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 