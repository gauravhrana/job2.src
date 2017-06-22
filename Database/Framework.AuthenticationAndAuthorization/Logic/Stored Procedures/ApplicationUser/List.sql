IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserList')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserList'
	DROP  Procedure  dbo.ApplicationUserList
END
GO

PRINT 'Creating Procedure ApplicationUserList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserList
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

CREATE Procedure dbo.ApplicationUserList
(
		@AuditId				INT		    = NULL		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUser'
)
AS
BEGIN
	
	SELECT	a.ApplicationUserId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName									
		,	a.FirstName	+ ' ' + a.MiddleName +	' ' + a.LastName	AS 'FullName'			
	FROM		dbo.ApplicationUser		a	
	ORDER BY	a.ApplicationUserId				ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= NULL
		,	@AuditAction					= 'List'
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO