--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDelete')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserDelete'
--	DROP  Procedure ApplicationUserDelete
--END
--GO

--PRINT 'Creating Procedure ApplicationUserDelete'
--GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserDelete
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

CREATE Procedure dbo.ApplicationUserDelete
(
		@ApplicationUserId 		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUser'			
)
AS
BEGIN

	DELETE	 dbo.ApplicationUser
	WHERE	 ApplicationUserId = @ApplicationUserId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType	
		,	@EntityKey						= @ApplicationUserId
		,	@AuditAction					= 'Delete' 
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		
END
GO
