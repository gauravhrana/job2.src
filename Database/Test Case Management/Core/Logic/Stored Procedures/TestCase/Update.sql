IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseUpdate')
BEGIN
	PRINT 'Dropping Procedure TestCaseUpdate'
	DROP  Procedure  TestCaseUpdate
END
GO

PRINT 'Creating Procedure TestCaseUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TestCaseUpdate
(
		@TestCaseId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestCase'
)
AS
BEGIN 

	UPDATE	dbo.TestCase 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestCaseId			=	@TestCaseId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestCaseId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO