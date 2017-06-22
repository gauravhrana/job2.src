IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationDetails'
	DROP  Procedure ApplicationOperationDetails
END
GO

PRINT 'Creating Procedure ApplicationOperationDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationOperationDetails
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

CREATE Procedure dbo.ApplicationOperationDetails
(
		@ApplicationOperationId		INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationOperation'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationOperationId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ApplicationOperationId			
		,	a.Name									
		,	a.Description						
		,	a.SortOrder						
		,	a.ApplicationId				
		,	a.SystemEntityTypeId	
		,	a.OperationValue
		,	b.Name					AS 'Application'			
		,	c.EntityName			AS 'SystemEntityType'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.ApplicationOperation			a	
	INNER JOIN	dbo.Application						b ON a.ApplicationId		= b.ApplicationId
	INNER JOIN	Configuration.dbo.SystemEntityType	c ON a.SystemEntityTypeId	= c.SystemEntityTypeId
	WHERE	ApplicationOperationId = @ApplicationOperationId	
	ORDER BY ApplicationOperationId
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
