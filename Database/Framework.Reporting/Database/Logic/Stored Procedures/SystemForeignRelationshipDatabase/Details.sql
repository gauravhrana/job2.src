IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseDetails')
BEGIN
  PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseDetails'
  DROP  Procedure SystemForeignRelationshipDatabaseDetails
END

GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseDetails'
GO
/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDatabaseDetails
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
CREATE Procedure dbo.SystemForeignRelationshipDatabaseDetails
(
		@SystemForeignRelationshipDatabaseId	INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SystemForeignRelationshipDatabaseId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	SystemForeignRelationshipDatabaseId
		,	ApplicationId			
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.SystemForeignRelationshipDatabase 
	WHERE	SystemForeignRelationshipDatabaseId = @SystemForeignRelationshipDatabaseId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipDatabase'
		,	@EntityKey				= @SystemForeignRelationshipDatabaseId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   
