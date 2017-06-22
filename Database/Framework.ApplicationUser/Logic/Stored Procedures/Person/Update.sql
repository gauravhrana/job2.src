IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonUpdate')
BEGIN
	PRINT 'Dropping Procedure PersonUpdate'
	DROP  Procedure  PersonUpdate
END
GO

PRINT 'Creating Procedure PersonUpdate'
GO

/******************************************************************************
**		File: 
**		Name: PersonUpdate
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

CREATE Procedure dbo.PersonUpdate
(
		@PersonId			INT					
	,	@FirstName			VARCHAR(50)				
	,	@LastName			VARCHAR(50)			
	,	@MiddleName			VARCHAR(50)		
	,	@PersonTitleId		INT				
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'Person'			
)
AS
BEGIN

	UPDATE	dbo.Person 
	SET		FirstName	=	@FirstName					
		,	LastName	=	@LastName		
		,	MiddleName  =   @MiddleName		
		,	PersonTitleId = @PersonTitleId					
	WHERE	PersonId	=	@PersonId	

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
 GO