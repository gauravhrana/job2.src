IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeDetails')
BEGIN
  PRINT 'Dropping Procedure SystemForeignRelationshipTypeDetails'
  DROP  Procedure SystemForeignRelationshipTypeDetails
END

GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeDetails'
GO
/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipTypeDetails
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SystemForeignRelationshipTypeDetails
(
		@SystemForeignRelationshipTypeId	INT					
	,	@AuditId							INT					
	,	@AuditDate							DATETIME		= NULL	
	,	@SystemEntityType					VARCHAR(50)		= 'SystemForeignRelationshipType'
)
AS
BEGIN
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SystemForeignRelationshipTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	SystemForeignRelationshipTypeId
		,	ApplicationId			
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.SystemForeignRelationshipType 
	WHERE	SystemForeignRelationshipTypeId = @SystemForeignRelationshipTypeId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipType'
		,	@EntityKey				= @SystemForeignRelationshipTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   
