IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameDelete')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameDelete'
	DROP  Procedure MenuDisplayNameDelete
END

GO

PRINT 'Creating Procedure MenuDisplayNameDelete'
GO


/******************************************************************************
**		File: 
**		Name: MenuDisplayNameDelete
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
CREATE Procedure dbo.MenuDisplayNameDelete
(
		@MenuDisplayNameId 		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'MenuDisplayName'
)
AS
BEGIN

	DELETE	 dbo.MenuDisplayName
	WHERE	 MenuDisplayNameId = @MenuDisplayNameId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@MenuDisplayNameId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO


