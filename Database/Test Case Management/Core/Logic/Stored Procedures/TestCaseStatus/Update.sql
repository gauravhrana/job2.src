IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure TestCaseStatusUpdate'
	DROP  Procedure  TestCaseStatusUpdate
END
GO

PRINT 'Creating Procedure TestCaseStatusUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseStatusUpdate
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

CREATE Procedure dbo.TestCaseStatusUpdate
(
		@TestCaseStatusId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseStatus'
)
AS
BEGIN 

	UPDATE	dbo.TestCaseStatus 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestCaseStatusId			=	@TestCaseStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestCaseStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO