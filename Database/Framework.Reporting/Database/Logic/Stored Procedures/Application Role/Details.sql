IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDetails'
	DROP  Procedure ApplicationRoleDetails
END
GO

PRINT 'Creating Procedure ApplicationRoleDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationRoleDetails
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

CREATE Procedure dbo.ApplicationRoleDetails
(
		@ApplicationRoleId		INT					
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationRoleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.ApplicationRoleId		
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
		,	a.ApplicationId
		,	b.Name				AS	'Application'
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'
	FROM		dbo.ApplicationRole a		
	INNER JOIN	Application		b ON a.ApplicationId = b.ApplicationId 
	WHERE	a.ApplicationRoleId = @ApplicationRoleId
	ORDER BY ApplicationRoleId	

	--Create Audit History
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO