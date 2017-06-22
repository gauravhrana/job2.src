IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailDetails')
BEGIN
  PRINT 'Dropping Procedure SuperKeyDetailDetails'
  DROP  Procedure SuperKeyDetailDetails
END

GO

PRINT 'Creating Procedure SuperKeyDetailDetails'
GO


/******************************************************************************
**		File: 
**		Name: SuperKeyDetailDetails
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

CREATE Procedure dbo.SuperKeyDetailDetails
(
		@SuperKeyDetailId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKeyDetail'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SuperKeyDetailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.SuperKeyDetailId
		,	a.ApplicationId	
		,	a.EntityKey
		,	a.SuperKeyId	
		,	b.Name					AS	'SuperKey'	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.SuperKeyDetail	a
	INNER JOIN	dbo.SuperKey		b	ON	a.SuperKeyId	=	b.SuperKeyId
	WHERE	SuperKeyDetailId = @SuperKeyDetailId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKeyDetail'
		,	@EntityKey				= @SuperKeyDetailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   