IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailDeleteHard'
	DROP  Procedure SearchKeyDetailDeleteHard
END
GO

PRINT 'Creating Procedure SearchKeyDetailDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SearchKeyDetailDelete
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
CREATE Procedure dbo.SearchKeyDetailDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetail'
)
AS
BEGIN

	IF @KeyType = 'SearchKeyDetailId'
	BEGIN

		DELETE	 dbo.SearchKeyDetail
		WHERE	 SearchKeyDetailId = @KeyId

	END

	
END
GO
