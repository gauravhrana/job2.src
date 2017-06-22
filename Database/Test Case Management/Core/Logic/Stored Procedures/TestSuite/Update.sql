IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteUpdate')
BEGIN
	PRINT 'Dropping Procedure TestSuiteUpdate'
	DROP  Procedure  TestSuiteUpdate
END
GO

PRINT 'Creating Procedure TestSuiteUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestSuiteUpdate
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

CREATE Procedure dbo.TestSuiteUpdate
(
		@TestSuiteId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestSuite'
)
AS
BEGIN 

	UPDATE	dbo.TestSuite 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestSuiteId			=	@TestSuiteId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestSuiteId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO