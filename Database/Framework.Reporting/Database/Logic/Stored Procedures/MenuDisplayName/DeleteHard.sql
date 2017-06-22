IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameDeleteHard'
	DROP  Procedure MenuDisplayNameDeleteHard
END

GO

PRINT 'Creating Procedure MenuDisplayNameDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: MenuDisplayNameDeleteHard
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
CREATE Procedure dbo.MenuDisplayNameDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)							
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'MenuDisplayName'
)
AS
BEGIN
	
	SET NOCOUNT ON

	IF @KeyType = 'MenuDisplayNameId'
		BEGIN

			DELETE	dbo.MenuDisplayName 
			WHERE	MenuDisplayNameId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			
			@SystemEntityType		= 'MenuDisplayName' 
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
