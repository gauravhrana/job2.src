IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDelete')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDelete'
	DROP  Procedure SuperKeyDelete
END
GO

PRINT 'Creating Procedure SuperKeyDelete'
GO
/******************************************************************************
**		File: 
**		Name: SuperKeyDelete
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
CREATE Procedure dbo.SuperKeyDelete
(
		@SuperKeyId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKey'
)
AS
BEGIN

	DELETE	 dbo.SuperKey
	WHERE	 SuperKeyId = @SuperKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
