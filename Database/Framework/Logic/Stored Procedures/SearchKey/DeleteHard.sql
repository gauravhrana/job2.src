IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDeleteHard'
	DROP  Procedure SearchKeyDeleteHard
END
GO

PRINT 'Creating Procedure SearchKeyDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SearchKeyDelete
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
CREATE Procedure dbo.SearchKeyDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKey'
)
AS
BEGIN

	IF @KeyType = 'SearchKeyId'
	BEGIN

		DELETE	 dbo.SearchKey
		WHERE	 SearchKeyId = @KeyId
	END

	
END
GO
