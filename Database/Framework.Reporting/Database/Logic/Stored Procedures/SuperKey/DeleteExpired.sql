IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDeleteExpired')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDeleteExpired'
	DROP  Procedure SuperKeyDeleteExpired
END
GO

PRINT 'Creating Procedure SuperKeyDeleteExpired'
GO
/******************************************************************************
**		File: 
**		Name: SuperKeyDeleteExpired
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SuperKeyDeleteExpired
(
		@ExpirationDate			DATETIME				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKey'
)
AS
BEGIN

	DELETE	dbo.SuperKeyDetail
	WHERE SuperKeyId IN
	(		
		SELECT	SuperKeyId
		FROM	dbo.SuperKey
		WHERE	ExpirationDate < @ExpirationDate
	)

	DELETE	dbo.SuperKey	
	WHERE	ExpirationDate < @ExpirationDate

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
