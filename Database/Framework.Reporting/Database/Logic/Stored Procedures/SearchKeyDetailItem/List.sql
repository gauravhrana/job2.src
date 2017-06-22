IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemList')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemList'
	DROP  Procedure  dbo.SearchKeyDetailItemList
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemList'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyDetailItemList
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
**     ----------					   ---------
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

CREATE Procedure dbo.SearchKeyDetailItemList
(
		@AuditId				INT					
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN

	SELECT  SearchKeyDetailItemId	  
		,	ApplicationId
		,	SearchKeyDetailId					  	
		,	Value	   
	FROM	dbo.SearchKeyDetailItem
	WHERE	ApplicationId = @ApplicationId
	ORDER BY	SearchKeyDetailItemId			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO