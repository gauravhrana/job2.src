IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ThemeDeleteHard'
	DROP  Procedure ThemeDeleteHard
END
GO

PRINT 'Creating Procedure ThemeDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ThemeDelete
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
CREATE Procedure dbo.ThemeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'Theme'
)
AS
BEGIN
	IF @KeyType = 'ThemeId'
	BEGIN

		DELETE	 dbo.Theme
		WHERE	 ThemeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
