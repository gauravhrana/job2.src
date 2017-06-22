IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemDetails')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemDetails'
	DROP  Procedure SearchKeyDetailItemDetails
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemDetails'
GO


/******************************************************************************
**		File: 
**		Name: SearchKeyDetailItemDetails
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

CREATE Procedure dbo.SearchKeyDetailItemDetails
(
		@SearchKeyDetailItemId			INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SearchKeyDetailItemId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	SearchKeyDetailItemId	
		,	ApplicationId		
		,	SearchKeyDetailId								
		,	Value						
		,	SortOrder						
		,	@LastUpdatedDate		AS	'UpdatedDate'	
		,   @LastUpdatedBy			AS	'UpdatedBy'		
		,	@LastAuditAction		AS	'LastAction'
	FROM	dbo.SearchKeyDetailItem 
	WHERE	SearchKeyDetailItemId = @SearchKeyDetailItemId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   