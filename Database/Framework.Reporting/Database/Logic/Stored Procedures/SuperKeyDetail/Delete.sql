IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailDelete')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailDelete'
	DROP  Procedure SuperKeyDetailDelete
END
GO

PRINT 'Creating Procedure SuperKeyDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: SuperKeyDetailDelete
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
CREATE Procedure dbo.SuperKeyDetailDelete
(
		@SuperKeyDetailId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKeyDetail'
)
AS
BEGIN

	DELETE	 dbo.SuperKeyDetail
	WHERE	 SuperKeyDetailId = @SuperKeyDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKeyDetail'
		,	@EntityKey				= @SuperKeyDetailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
