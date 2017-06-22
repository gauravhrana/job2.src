IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonDelete')
BEGIN
	PRINT 'Dropping Procedure PersonDelete'
	DROP  Procedure PersonDelete
END
GO

PRINT 'Creating Procedure PersonDelete'
GO

/******************************************************************************
**		File: 
**		Name: PersonDelete
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

CREATE Procedure dbo.PersonDelete
(
		@PersonId 			INT					
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'Person'			
)
AS
BEGIN

	DELETE	 dbo.Person
	WHERE	 PersonId = @PersonId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		
END
GO
