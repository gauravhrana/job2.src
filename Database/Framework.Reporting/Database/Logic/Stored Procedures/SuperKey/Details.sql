IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetails')
BEGIN
  PRINT 'Dropping Procedure SuperKeyDetails'
  DROP  Procedure SuperKeyDetails
END

GO

PRINT 'Creating Procedure SuperKeyDetails'
GO


/******************************************************************************
**		File: 
**		Name: SuperKeyDetails
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

CREATE Procedure dbo.SuperKeyDetails
(
		@SuperKeyId					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKey'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SuperKeyId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	SuperKeyId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder	
		,	SystemEntityTypeId
		,	ExpirationDate	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.SuperKey 
	WHERE	SuperKeyId = @SuperKeyId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   