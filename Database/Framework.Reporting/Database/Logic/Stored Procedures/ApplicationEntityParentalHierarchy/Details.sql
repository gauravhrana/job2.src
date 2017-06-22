IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyDetails'
	DROP  Procedure ApplicationEntityParentalHierarchyDetails
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyDetails
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

CREATE Procedure dbo.ApplicationEntityParentalHierarchyDetails
(
		@ApplicationEntityParentalHierarchyId	INT	
	,	@AuditId								INT					
	,	@AuditDate								DATETIME = NULL		
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationEntityParentalHierarchyId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	ApplicationEntityParentalHierarchyId
		,	ApplicationId			
		,	Name					
		,	Description		
		,	SortOrder	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'	
	FROM	dbo.ApplicationEntityParentalHierarchy 
	WHERE	ApplicationEntityParentalHierarchyId = @ApplicationEntityParentalHierarchyId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   