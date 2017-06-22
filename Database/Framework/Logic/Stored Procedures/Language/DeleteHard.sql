IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'LanguageDeleteHard')
BEGIN
	PRINT 'Dropping Procedure LanguageDeleteHard'
	DROP  Procedure LanguageDeleteHard
END
GO

PRINT 'Creating Procedure LanguageDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: LanguageDelete
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
CREATE Procedure dbo.LanguageDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'Language'
)
AS
BEGIN
	IF @KeyType = 'LanguageId'
	BEGIN

		DELETE	 dbo.Language
		WHERE	 LanguageId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
