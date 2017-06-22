IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseOwnerUpdate')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerUpdate'
	DROP  Procedure  TestCaseOwnerUpdate
END
GO

PRINT 'Creating Procedure TestCaseOwnerUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseOwnerUpdate
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

CREATE Procedure dbo.TestCaseOwnerUpdate
(
		@TestCaseOwnerId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseOwner'
)
AS
BEGIN 

	UPDATE	dbo.TestCaseOwner 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestCaseOwnerId			=	@TestCaseOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestCaseOwnerId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO