IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyChildrenGet')
BEGIN
	PRINT 'Dropping Procedure SearchKeyChildrenGet'
	DROP  Procedure SearchKeyChildrenGet
END
GO

PRINT 'Creating Procedure SearchKeyChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: SearchKeyChildrenGet
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

CREATE Procedure dbo.SearchKeyChildrenGet
(
		@SearchKeyId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'SearchKey'
)
AS
BEGIN

	-- GET SearchKeyDetail Records
	SELECT	b.[View]
		,	a.ApplicationId			
		,	a.SearchKeyId	
		,	b.Name					AS	'SearchKey'		
	FROM		dbo.SearchKeyDetail	a
	INNER JOIN	dbo.SearchKey		b	ON	a.SearchKeyId	=	b.SearchKeyId
	WHERE	a.SearchKeyId = @SearchKeyId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   