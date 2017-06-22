IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationDetails'
	DROP  Procedure ApplicationDetails
END
GO

PRINT 'Creating Procedure ApplicationDetails'
GO

/******************************************************************************
**		Task: 
**		Name: ApplicationDetails
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

CREATE Procedure dbo.ApplicationDetails
(
		@ApplicationId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Application'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	ApplicationId			
		,	Name							
		,	Description				
		,	SortOrder	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'							
	FROM	dbo.Application 
	WHERE	ApplicationId = @ApplicationId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   