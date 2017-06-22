IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonDoesExist')
BEGIN
	PRINT 'Dropping Procedure PersonDoesExist'
	DROP  Procedure  PersonDoesExist
END
GO

PRINT 'Creating Procedure PersonDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: PersonDoesExist
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.PersonDoesExist
(	
		@ApplicationId			INT				
	,	@LastName				VARCHAR(50)		= NULL	
	,	@FirstName				VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
    ,	@SystemEntityType		VARCHAR(50)		= 'Person'	
)
AS
BEGIN	
		
	SELECT		*
	FROM		dbo.Person
	WHERE		LastName	= @LastName
	AND			FirstName	= @FirstName
	AND			ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Person'
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
END
GO
