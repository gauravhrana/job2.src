IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDelete')
BEGIN
	PRINT 'Dropping Procedure MenuDelete'
	DROP  Procedure MenuDelete
END

GO

PRINT 'Creating Procedure MenuDelete'
GO


/******************************************************************************
**		File: 
**		Name: MenuDelete
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
CREATE Procedure dbo.MenuDelete
(
		@MenuId 				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'Menu'
)
AS
BEGIN

	DELETE	 dbo.Menu
	WHERE	 MenuId = @MenuId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@MenuId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO


