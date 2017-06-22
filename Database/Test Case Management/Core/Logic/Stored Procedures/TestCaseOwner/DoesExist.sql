IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestCaseOwnerDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerDoesExist'
	DROP  Procedure  TestCaseOwnerDoesExist
END
GO

PRINT 'Creating Procedure TestCaseOwnerDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseOwnerDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TestCaseOwnerDoesExist
(
		@TestCaseOwnerId			INT							
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCaseOwner'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TestCaseOwner a
	WHERE		a.Name			=	@Name 
	AND			a.ApplicationId	=	@ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

