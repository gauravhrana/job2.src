IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestCaseDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestCaseDoesExist'
	DROP  Procedure  TestCaseDoesExist
END
GO

PRINT 'Creating Procedure TestCaseDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseDoesExist
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

Create procedure dbo.TestCaseDoesExist
(
		@TestCaseId			INT							
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCase'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TestCase a
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

