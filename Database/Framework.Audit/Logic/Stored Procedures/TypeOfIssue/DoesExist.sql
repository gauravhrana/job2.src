IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TypeOfIssueDoesExist')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueDoesExist'
	DROP  Procedure  TypeOfIssueDoesExist
END
GO

PRINT 'Creating Procedure TypeOfIssueDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TypeOfIssueDoesExist
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TypeOfIssueDoesExist
(
		@TypeOfIssueId			INT				= NULL
	,	@Name					VARCHAR(50)		= NULL		
	,	@ApplicationId			INT
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'TypeOfIssue'
)
AS
BEGIN


	SELECT		a.*
	FROM		dbo.TypeOfIssue a
	WHERE		a.Name = @Name
	AND		a.ApplicationId = @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TypeOfIssue'
		,	@EntityKey				= @TypeOfIssueId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO

