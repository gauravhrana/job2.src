IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailList')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailList'
	DROP  Procedure  dbo.SearchKeyDetailList
END
GO

PRINT 'Creating Procedure SearchKeyDetailList'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyDetailList
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SearchKeyDetailList
(
		@ApplicationId			INT
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetail'
)
AS
BEGIN

	SELECT	a.SearchKeyDetailId
		,	a.ApplicationId	
		,	a.SearchParameter
		,	a.SearchKeyId	
		,	b.Name					AS	'SearchKey'		
		,	a.SortOrder
	FROM		dbo.SearchKeyDetail	a
	INNER JOIN	dbo.SearchKey		b	ON	a.SearchKeyId	=	b.SearchKeyId
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY SearchKeyDetailId			ASC
		,	 SortOrder			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO