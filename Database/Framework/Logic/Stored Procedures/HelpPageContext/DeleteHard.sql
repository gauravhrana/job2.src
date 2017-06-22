IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextDeleteHard')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextDeleteHard'
	DROP  Procedure HelpPageContextDeleteHard
END
GO

PRINT 'Creating Procedure HelpPageContextDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: HelpPageContextDelete
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
CREATE Procedure dbo.HelpPageContextDeleteHard
(
		@KeyId 					INT				
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'HelpPageContext'
)
AS
BEGIN
		
	IF @KeyType = 'HelpPageContextId'
	BEGIN				

		DELETE	 dbo.HelpPageContext
		WHERE	 HelpPageContextId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO