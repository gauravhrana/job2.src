IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusDetails')
BEGIN
  PRINT 'Dropping Procedure FeatureOwnerStatusDetails'
  DROP  Procedure FeatureOwnerStatusDetails
END

GO

PRINT 'Creating Procedure FeatureOwnerStatusDetails'
GO


/******************************************************************************
**		File: 
**		Name: FeatureOwnerStatusDetails
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

CREATE Procedure dbo.FeatureOwnerStatusDetails
(
		@FeatureOwnerStatusId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'FeatureOwnerStatus'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@FeatureOwnerStatusId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	FeatureOwnerStatusId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.FeatureOwnerStatus 
	WHERE	FeatureOwnerStatusId = @FeatureOwnerStatusId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FeatureOwnerStatus'
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   