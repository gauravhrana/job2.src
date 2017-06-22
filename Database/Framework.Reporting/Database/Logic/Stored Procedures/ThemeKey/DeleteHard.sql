IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeKeyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ThemeKeyDeleteHard'
	DROP  Procedure ThemeKeyDeleteHard
END
GO

PRINT 'Creating Procedure ThemeKeyDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ThemeKeyDelete
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
CREATE Procedure dbo.ThemeKeyDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeKey'
)
AS
BEGIN
	IF @KeyType = 'ThemeKeyId'
	BEGIN

		DELETE	 dbo.ThemeKey
		WHERE	 ThemeKeyId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
