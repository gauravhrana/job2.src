IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeDeleteHard'
	DROP  Procedure ApplicationModeDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationModeDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationModeDelete
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
CREATE Procedure dbo.ApplicationModeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMode'
)
AS
BEGIN
	IF @KeyType = 'ApplicationModeId'
	BEGIN

		DELETE	 dbo.ApplicationMode
		WHERE	 ApplicationModeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
