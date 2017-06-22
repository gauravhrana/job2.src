IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonDetails')
BEGIN
	PRINT 'Dropping Procedure PersonDetails'
	DROP  Procedure PersonDetails
END
GO

PRINT 'Creating Procedure PersonDetails'
GO


/******************************************************************************
**		File: 
**		Name: Student_Details
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
**		-------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PersonDetails
(
		@PersonId		         INT
	,	@ApplicationId			 INT
	,   @AuditId				 INT			
    ,   @AuditDate				 DATETIME		= NULL
	,   @SystemEntityType		 VARCHAR(50)	= 'Person'
)
	
AS
BEGIN
	SELECT	PersonId
		,	ApplicationId
		,	FirstName
		,   LastName									
	FROM	Person 
	WHERE	PersonId	  = @PersonId	
	AND		ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Person'
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   