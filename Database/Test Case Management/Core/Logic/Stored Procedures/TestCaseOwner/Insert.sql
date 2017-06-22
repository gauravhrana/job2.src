IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseOwnerInsert')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerInsert'
	DROP  Procedure TestCaseOwnerInsert
END
GO

PRINT 'Creating Procedure TestCaseOwnerInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestCaseOwnerInsert
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

CREATE Procedure dbo.TestCaseOwnerInsert
(
		@TestCaseOwnerId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseOwner'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestCaseOwnerId OUTPUT, @AuditId
		
	INSERT INTO dbo.TestCaseOwner 
	( 
			TestCaseOwnerId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TestCaseOwnerId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseOwnerId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 