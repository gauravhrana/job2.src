IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseStatusInsert')
BEGIN
	PRINT 'Dropping Procedure TestCaseStatusInsert'
	DROP  Procedure TestCaseStatusInsert
END
GO

PRINT 'Creating Procedure TestCaseStatusInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestCaseStatusInsert
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

CREATE Procedure dbo.TestCaseStatusInsert
(
		@TestCaseStatusId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseStatus'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestCaseStatusId OUTPUT, @AuditId
		
	INSERT INTO dbo.TestCaseStatus 
	( 
			TestCaseStatusId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TestCaseStatusId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 