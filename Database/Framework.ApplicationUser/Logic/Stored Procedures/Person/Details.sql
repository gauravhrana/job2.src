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
**		Name: PersonDetails
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

CREATE Procedure dbo.PersonDetails
(
		@PersonId			INT					
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'Person'			
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@PersonId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.PersonId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.PersonTitleId	
		,	b.Name													AS 'PersonTitle'								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS 'FullName'
		,	@LastUpdatedDate										AS	'UpdatedDate'
		,	@LastUpdatedBy											AS	'UpdatedBy'
		,	@LastAuditAction										AS	'LastAction'			
	FROM		dbo.Person		a
	INNER JOIN 	PersonTitle	b	ON a.PersonTitleId = b.PersonTitleId
	WHERE		a.PersonId = @PersonId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO
   