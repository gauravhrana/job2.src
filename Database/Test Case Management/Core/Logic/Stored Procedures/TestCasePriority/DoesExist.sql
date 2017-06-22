IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestCasePriorityDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestCasePriorityDoesExist'
	DROP  Procedure  TestCasePriorityDoesExist
END
GO

PRINT 'Creating Procedure TestCasePriorityDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestCasePriorityDoesExist
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

Create procedure dbo.TestCasePriorityDoesExist
(
		@TestCasePriorityId			INT							
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCasePriority'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TestCasePriority a
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

