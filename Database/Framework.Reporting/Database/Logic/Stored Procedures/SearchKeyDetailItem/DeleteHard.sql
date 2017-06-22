IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemDeleteHard'
	DROP  Procedure SearchKeyDetailItemDeleteHard
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: SearchKeyDetailItemDelete
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
CREATE Procedure dbo.SearchKeyDetailItemDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN

	IF @KeyType = 'SearchKeyDetailItemId'
	BEGIN
		DELETE	 dbo.SearchKeyDetailItem
		WHERE	 SearchKeyDetailItemId = @KeyId
	END

END
GO
