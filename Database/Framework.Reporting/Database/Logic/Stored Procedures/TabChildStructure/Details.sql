IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureDetails')
BEGIN
  PRINT 'Dropping Procedure TabChildStructureDetails'
  DROP  Procedure TabChildStructureDetails
END

GO

PRINT 'Creating Procedure TabChildStructureDetails'
GO


/******************************************************************************
**		File: 
**		Name: TabChildStructureDetails
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
**		Date:		Author:				EntityName:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TabChildStructureDetails
(
		@TabChildStructureId		INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TabChildStructure'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TabChildStructureId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.TabChildStructureId			
		,	a.ApplicationId
		,	a.Name						
		,	a.EntityName			
		,	a.SortOrder	
		,	a.TabParentStructureId	
		,	a.InnerControlPath
		,	b.Name					AS	'TabParentStructure'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.TabChildStructure		a
	INNER JOIN	dbo.TabParentStructure		b	ON	a.TabParentStructureId	=	b.TabParentStructureId
	WHERE	TabChildStructureId		= @TabChildStructureId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabChildStructure'
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   