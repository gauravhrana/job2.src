IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesDeleteHard')
BEGIN
	PRINT 'Dropping Procedure AboutPagesDeleteHard'
	DROP  Procedure AboutPagesDeleteHard
END
GO

PRINT 'Creating Procedure AboutPagesDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: AboutPagesDeleteHard
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

CREATE Procedure dbo.AboutPagesDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'
)
AS
BEGIN

	IF @KeyType = 'AboutPagesId'
		BEGIN 
	
			DELETE	dbo.AboutPages
			WHERE	AboutPagesId = @KeyId

END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType			= @SystemEntityType
		,	@EntityKey					= @KeyId
		,	@AuditAction				= 'DeleteHard'
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId			= @AuditId
		
END
GO
