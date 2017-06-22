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
**		----------						-----------
**
**		Auth:
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PersonUpdate
(      
		@PersonId		    INT       
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId            INT		
    ,   @AuditDate          DATETIME	= NULL 
	,   @SystemEntityType	VARCHAR(50)	= 'Person'	
)
AS
 BEGIN
	UPDATE	Person
	SET		PersonId                  = @PersonId      					
		,	LastName				   = @LastName		 
		,	FirstName				   = @FirstName	
	WHERE	PersonId		     	   = @PersonId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= 'Person' 
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

