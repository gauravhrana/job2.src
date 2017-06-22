IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestSuiteDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestSuiteDoesExist'
	DROP  Procedure  TestSuiteDoesExist
END
GO

PRINT 'Creating Procedure TestSuiteDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestSuiteDoesExist
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

Create procedure dbo.TestSuiteDoesExist
(
		@TestSuiteId			INT							
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestSuite'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TestSuite a
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

