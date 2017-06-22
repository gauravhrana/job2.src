IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseIssueTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseIssueTypeDeleteHard'
	DROP  Procedure ReleaseIssueTypeDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseIssueTypeDeleteHard'
GO
/******************************************************************************
**		Task: 
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
CREATE Procedure dbo.ReleaseIssueTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseIssueType'	
)
AS
BEGIN
		IF @KeyType = 'ReleaseIssueTypeId'
		BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'ReleaseIssueTypeId'  
			,	@AuditId	=	@AuditId

		DELETE	 dbo.ReleaseIssueType
		WHERE	 ReleaseIssueTypeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
