IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestCaseStatusDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestCaseStatusDoesExist'
	DROP  Procedure  TestCaseStatusDoesExist
END
GO

PRINT 'Creating Procedure TestCaseStatusDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseStatusDoesExist
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

Create procedure dbo.TestCaseStatusDoesExist
(
		@TestCaseStatusId			INT							
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCaseStatus'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TestCaseStatus a
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

