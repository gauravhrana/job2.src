IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseIssueTypeDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseIssueTypeDelete'
	DROP  Procedure ReleaseIssueTypeDelete
END
GO

PRINT 'Creating Procedure ReleaseIssueTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseIssueTypeDelete
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
CREATE Procedure dbo.ReleaseIssueTypeDelete
(
		@ReleaseIssueTypeId 			    INT						
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL		
	,	@SystemEntityType					VARCHAR(50)	= 'ReleaseIssueType'
)
AS
BEGIN
		DELETE	 dbo.ReleaseIssueType
		WHERE	 ReleaseIssueTypeId = @ReleaseIssueTypeId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseIssueType'
		,	@EntityKey				= @ReleaseIssueTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
GO
