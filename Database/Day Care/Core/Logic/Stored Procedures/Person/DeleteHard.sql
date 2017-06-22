IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonDeleteHard')
BEGIN
	PRINT 'Dropping Procedure PersonDeleteHard'
	DROP  Procedure PersonDeleteHard
END
GO

PRINT 'Creating Procedure PersonDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: PersonDeleteHard
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

CREATE Procedure dbo.PersonDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Person'	
)
AS

BEGIN

	IF @KeyType = 'PersonId'
		BEGIN 

			EXEC	dbo.PersonDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'PersonId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.Person
			WHERE	PersonId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType			= 'Person'
		,	@EntityKey					= @KeyId
		,	@AuditAction				= 'DeleteHard'
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId			= @AuditId
		
END
GO
