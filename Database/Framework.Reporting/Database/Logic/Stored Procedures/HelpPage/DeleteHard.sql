IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageDeleteHard')
BEGIN
	PRINT 'Dropping Procedure HelpPageDeleteHard'
	DROP  Procedure HelpPageDeleteHard
END
GO

PRINT 'Creating Procedure HelpPageDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: HelpPageDelete
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
CREATE Procedure dbo.HelpPageDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'HelpPage'
)
AS
BEGIN

	IF @KeyType = 'HelpPageId'
	BEGIN

		DELETE	 dbo.HelpPage
		WHERE	 HelpPageId = @KeyId

	END

	
END
GO
