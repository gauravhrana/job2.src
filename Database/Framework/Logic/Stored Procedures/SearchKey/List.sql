IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyList')
BEGIN
	PRINT 'Dropping Procedure SearchKeyList'
	DROP  Procedure  dbo.SearchKeyList
END
GO

PRINT 'Creating Procedure SearchKeyList'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyList
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

CREATE Procedure dbo.SearchKeyList
(
		@ApplicationId			INT
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKey'
)
AS
BEGIN

	SELECT	a.SearchKeyId
		,	a.ApplicationId					
		,	a.Name						
		,	a.[Description]					
		,	a.SortOrder
		,	a.[View]		
	FROM	dbo.SearchKey a 
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY SearchKeyId			ASC
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