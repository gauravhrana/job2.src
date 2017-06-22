IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonList')
BEGIN
	PRINT 'Dropping Procedure PersonList'
	DROP PROCEDURE PersonList
END
GO

PRINT 'Creating Procedure PersonList'
GO

/******************************************************************************
**		File: 
**		Name: PersonList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PersonList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Person'
)
AS
BEGIN
		SELECT	PersonId	
			,	ApplicationId   
			,	LastName		  	
			,	FirstName
		FROM	dbo.Person 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY PersonId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
