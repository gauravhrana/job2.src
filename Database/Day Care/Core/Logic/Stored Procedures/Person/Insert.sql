IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonInsert')
BEGIN
	PRINT 'Dropping Procedure PersonInsert'
	DROP   Procedure PersonInsert
END
GO

PRINT 'Creating Procedure PersonInsert'
GO

/******************************************************************************
**		File: 
**		Name: PersonInsert
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
CREATE Procedure dbo.PersonInsert
(
		@PersonId		    INT				= NULL 	OUTPUT
	,	@ApplicationId		INT		 
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId		    INT		
    ,   @AuditDate		    DATETIME		= NULL
	,   @SystemEntityType	VARCHAR(50)		= 'Person'		
)
AS
BEGIN

IF @PersonId IS NULL OR @PersonId = -9999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'PersonId', @PersonId OUTPUT
		END	
	INSERT INTO dbo.Person
	(
			 PersonId
		,	 ApplicationId
		,	 LastName
		,	 FirstName
	)
	VALUES
	(
			@PersonId
		,	@ApplicationId
		,	@LastName
		,	@FirstName
	)

--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Person' 
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO
