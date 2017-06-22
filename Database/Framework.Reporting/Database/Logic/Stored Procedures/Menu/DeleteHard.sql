IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MenuDeleteHard'
	DROP  Procedure MenuDeleteHard
END

GO

PRINT 'Creating Procedure MenuDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: MenuDeleteHard
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
CREATE Procedure dbo.MenuDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)							
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'Menu'
)
AS
BEGIN
	
	SET NOCOUNT ON

	IF @KeyType = 'MenuId'
		BEGIN

			DELETE	dbo.Menu 
			WHERE	MenuId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			
			@SystemEntityType		= 'Menu' 
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
