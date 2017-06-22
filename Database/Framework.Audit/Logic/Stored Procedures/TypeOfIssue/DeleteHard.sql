IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TypeOfIssueDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueDeleteHard'
	DROP  Procedure TypeOfIssueDeleteHard
END
GO

PRINT 'Creating Procedure TypeOfIssueDeleteHard'
GO
/******************************************************************************
**		Task: 
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
CREATE Procedure dbo.TypeOfIssueDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'TypeOfIssue'
)
AS
BEGIN
	IF @KeyType = 'TypeOfIssueId'
	BEGIN

		DELETE	 dbo.TypeOfIssue
		WHERE	 TypeOfIssueId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
