IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonList')
BEGIN
	PRINT 'Dropping Procedure PersonList'
	DROP  Procedure  dbo.PersonList
END
GO

PRINT 'Creating Procedure PersonList'
GO

/******************************************************************************
**		File: 
**		Name: PersonList
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
**     ----------					   ---------
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

CREATE Procedure dbo.PersonList
(
		@AuditId				INT		    = NULL		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Person'
)
AS
BEGIN
	
	SELECT	a.PersonId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.PersonTitleId	
		,	b.Name													AS 'PersonTitle'								
		,	a.FirstName	+ ' ' + a.MiddleName +	' ' + a.LastName	AS 'FullName'			
	FROM		dbo.Person		a	
	INNER JOIN 	PersonTitle	b	ON a.PersonTitleId = b.PersonTitleId
	ORDER BY	a.PersonId				ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
	
GO