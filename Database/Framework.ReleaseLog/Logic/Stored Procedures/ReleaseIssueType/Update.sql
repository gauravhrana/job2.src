IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseIssueTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseIssueTypeUpdate'
	DROP  Procedure  ReleaseIssueTypeUpdate
END
GO

PRINT 'Creating Procedure ReleaseIssueTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseIssueTypeUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ReleaseIssueTypeUpdate
(
		@ReleaseIssueTypeId		INT 			
	,	@Name					VARCHAR(50)				
	,	@Description            VARCHAR (500)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseIssueType'
)
AS
BEGIN
 
 	UPDATE	dbo.ReleaseIssueType 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder						
	WHERE	ReleaseIssueTypeId		=	@ReleaseIssueTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseIssueType'
		,	@EntityKey				= @ReleaseIssueTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO