IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDetails'
	DROP  Procedure ApplicationUserDetails
END
GO

PRINT 'Creating Procedure ApplicationUserDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserDetails
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

CREATE Procedure dbo.ApplicationUserDetails
(
		@ApplicationUserId			INT					
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'ApplicationUser'			
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationUserId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ApplicationUserId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS 'FullName'
		,	@LastUpdatedDate										AS	'UpdatedDate'
		,	@LastUpdatedBy											AS	'UpdatedBy'
		,	@LastAuditAction										AS	'LastAction'			
	FROM		dbo.ApplicationUser		a
	WHERE		a.ApplicationUserId = @ApplicationUserId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO
   