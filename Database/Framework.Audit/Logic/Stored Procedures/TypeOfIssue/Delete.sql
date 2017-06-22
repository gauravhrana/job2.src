IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TypeOfIssueDelete')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueDelete'
	DROP  Procedure TypeOfIssueDelete
END
GO

PRINT 'Creating Procedure TypeOfIssueDelete'
GO
/******************************************************************************
**		File: 
**		Name: TypeOfIssueDelete
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TypeOfIssueDelete
(
		@TypeOfIssueId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TypeOfIssue'
)
AS
BEGIN

	DELETE	 dbo.TypeOfIssue
	WHERE	 TypeOfIssueId = @TypeOfIssueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TypeOfIssue'
		,	@EntityKey				= @TypeOfIssueId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
