IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NeedsDeleteHard'
	DROP  Procedure NeedsDeleteHard
END
GO

PRINT 'Creating Procedure NeedsDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: NeedsDelete
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

CREATE Procedure dbo.NeedsDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Needs'	
)
AS
BEGIN

	IF @KeyType = 'NeedsId'
		BEGIN

			DELETE	 dbo.Needs
			WHERE	 NeedsId = @KeyId	

		END
	ELSE IF @KeyType = 'NeedItemId'
		BEGIN

			DELETE	 dbo.Needs
			WHERE	 NeedItemId = @KeyId

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Needs
			WHERE	 StudentId = @KeyId

		END  	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Needs'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
