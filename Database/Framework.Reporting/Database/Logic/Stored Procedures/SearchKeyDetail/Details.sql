IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailDetails')
BEGIN
  PRINT 'Dropping Procedure SearchKeyDetailDetails'
  DROP  Procedure SearchKeyDetailDetails
END

GO

PRINT 'Creating Procedure SearchKeyDetailDetails'
GO


/******************************************************************************
**		File: 
**		Name: SearchKeyDetailDetails
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

CREATE Procedure dbo.SearchKeyDetailDetails
(
		@SearchKeyDetailId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetail'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SearchKeyDetailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.SearchKeyDetailId
		,	a.ApplicationId	
		,	a.SearchParameter
		,	a.SearchKeyId	
		,	b.Name					AS	'SearchKey'	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.SearchKeyDetail	a
	INNER JOIN	dbo.SearchKey		b	ON	a.SearchKeyId	=	b.SearchKeyId
	WHERE	SearchKeyDetailId = @SearchKeyDetailId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKeyDetail'
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   