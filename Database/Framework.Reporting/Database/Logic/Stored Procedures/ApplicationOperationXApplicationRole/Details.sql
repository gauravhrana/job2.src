IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDetails'
	DROP  Procedure ApplicationOperationXApplicationRoleDetails
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleDetails
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

CREATE Procedure dbo.ApplicationOperationXApplicationRoleDetails
(
		@ApplicationOperationXApplicationRoleId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@ApplicationOperationId						INT			= NULL	
	,	@ApplicationRoleId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationOperationXApplicationRoleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ApplicationOperationXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationOperationId						
		,	a.ApplicationRoleId								
		,	b.Name				AS	'ApplicationOperation'			
		,	c.Name				AS	'ApplicationRole'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.ApplicationOperationXApplicationRole	a
	INNER JOIN	dbo.ApplicationOperation			b	ON	a.ApplicationOperationId	=	b.ApplicationOperationId
	INNER JOIN	dbo.ApplicationRole					c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationOperationXApplicationRoleId	=	ISNULL(@ApplicationOperationXApplicationRoleId,	a.ApplicationOperationXApplicationRoleId)	
	AND		a.ApplicationOperationId					=	ISNULL(@ApplicationOperationId,			a.ApplicationOperationId)
	AND		a.ApplicationRoleId							=	ISNULL(@ApplicationRoleId,			a.ApplicationRoleId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationXApplicationRoleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   