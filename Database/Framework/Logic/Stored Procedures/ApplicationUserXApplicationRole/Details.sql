IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDetails'
	DROP  Procedure ApplicationUserXApplicationRoleDetails
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleDetails
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

CREATE Procedure dbo.ApplicationUserXApplicationRoleDetails
(
		@ApplicationUserXApplicationRoleId		INT			= NULL	
	,	@AuditId								INT	        = NULL				
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationUserXApplicationRoleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ApplicationUserXApplicationRoleId	
		,	a.ApplicationId
		,	a.ApplicationUserId						
		,	a.ApplicationRoleId								
		,	b.FirstName												AS	'ApplicationUser'	
		,	b.FirstName	+ ' ' + b.LastName +	' ' + b.LastName	AS	'FullName'			
		,	c.Name													AS	'ApplicationRole'	
		,	@LastUpdatedDate										AS	'UpdatedDate'
		,	@LastUpdatedBy											AS	'UpdatedBy'
		,	@LastAuditAction										AS	'LastAction'					
	FROM		dbo.ApplicationUserXApplicationRole	a
	INNER JOIN	dbo.ApplicationUser					b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	dbo.ApplicationRole					c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationUserXApplicationRoleId	=	ISNULL(@ApplicationUserXApplicationRoleId,	a.ApplicationUserXApplicationRoleId)	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserXApplicationRoleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   